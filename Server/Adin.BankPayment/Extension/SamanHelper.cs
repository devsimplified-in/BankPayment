﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Adin.BankPayment.Connector.Enum;
using Adin.BankPayment.Connector.Model;
using Adin.BankPayment.Domain.Model;
using Adin.BankPayment.Saman;
using Adin.BankPayment.Service;
using Microsoft.Extensions.Logging;

namespace Adin.BankPayment.Extension
{
    public class SamanHelper : IBankHelper
    {
        private readonly IRepository<ApplicationBank> _applicationBankRepository;
        private readonly ILogger _logger;
        private readonly IRepository<Transaction> _transactionRepository;

        public SamanHelper(ILogger logger,
            IRepository<Transaction> transactionRepository,
            IRepository<ApplicationBank> applicationBankRepository)
        {
            _logger = logger;
            _transactionRepository = transactionRepository;
            _applicationBankRepository = applicationBankRepository;
        }

        public async Task<VerifyTransactionResponseModel> VerifyTransaction(Transaction transaction)
        {
            var verifyTransactionResult = new VerifyTransactionResponseModel();

            var applicationBanks = await _applicationBankRepository.GetFirstBy(x =>
                x.BankId == transaction.BankId && x.ApplicationId == transaction.ApplicationId);
            var samanParams = applicationBanks.ApplicationBankParams.ToList();
            var midParam = samanParams.FirstOrDefault(x => x.ParamKey == "MID");

            if (midParam == null)
                return verifyTransactionResult;

            var samanGateway = new SamanGateway(midParam.ParamValue);

            var result = await samanGateway.VerifyTransactionAsync(transaction.BankTrackCode);

            if (result > 0)
            {
                _logger.LogDebug("Verify Done");
                var message = "بانک صحت رسید دیجیتالی شما را تصدیق نمود. فرایند خرید تکمیل گشت";
                message += "<br/>" + " شماره رسید : " + transaction.BankTrackCode;
                transaction.Status = (byte)TransactionStatusEnum.Success;
                transaction.ModifiedOn = DateTime.Now;
                transaction.ModifiedBy = 1;
                await _transactionRepository.Update(transaction);

                verifyTransactionResult.Status = true;
                verifyTransactionResult.ErrorCode = (byte)ErrorCodeEnum.NoError;
                verifyTransactionResult.Message = message;

                return verifyTransactionResult;
            }

            var errorMsg = TransactionChecking((int)result);
            _logger.LogDebug("Result code: " + result);
            transaction.Status = (byte)TransactionStatusEnum.ErrorOnVerify;
            transaction.BankErrorCode = Convert.ToInt32(result);
            transaction.BankErrorMessage = errorMsg;
            transaction.ModifiedOn = DateTime.Now;
            transaction.ModifiedBy = 1;
            await _transactionRepository.Update(transaction);

            verifyTransactionResult.Status = false;
            verifyTransactionResult.ErrorCode = (byte)ErrorCodeEnum.VerifyError;
            verifyTransactionResult.Message = errorMsg;

            return verifyTransactionResult;
        }


        private static string TransactionChecking(int i)
        {
            var errorMsg = "";
            switch (i)
            {
                case -1: //TP_ERROR
                    errorMsg = "بروز خطا درهنگام بررسی صحت رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-1";
                    break;
                case -2: //ACCOUNTS_DON'T_MATCH
                    errorMsg = "بروز خطا در هنگام تایید رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-2";
                    break;
                case -3: //BAD_INPUT
                    errorMsg = "خطا در پردازش رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-3";
                    break;
                case -4: //INVALID_PASSWORD_OR_ACCOUNT
                    errorMsg = "خطای درونی سیستم درهنگام بررسی صحت رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-4";
                    break;
                case -5: //DATABASE_EXCEPTION
                    errorMsg = "خطای درونی سیستم درهنگام بررسی صحت رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-5";
                    break;
                case -7: //ERROR_STR_NULL
                    errorMsg = "خطا در پردازش رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-7";
                    break;
                case -8: //ERROR_STR_TOO_LONG
                    errorMsg = "خطا در پردازش رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-8";
                    break;
                case -9: //ERROR_STR_NOT_AL_NUM
                    errorMsg = "خطا در پردازش رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-9";
                    break;
                case -10: //ERROR_STR_NOT_BASE64
                    errorMsg = "خطا در پردازش رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-10";
                    break;
                case -11: //ERROR_STR_TOO_SHORT
                    errorMsg = "خطا در پردازش رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-11";
                    break;
                case -12: //ERROR_STR_NULL
                    errorMsg = "خطا در پردازش رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-12";
                    break;
                case -13: //ERROR IN AMOUNT OF REVERS TRANSACTION AMOUNT
                    errorMsg = "خطا در پردازش رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-13";
                    break;
                case -14: //INVALID TRANSACTION
                    errorMsg = "خطا در پردازش رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-14";
                    break;
                case -15: //RETERNED AMOUNT IS WRONG
                    errorMsg = "خطا در پردازش رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-15";
                    break;
                case -16: //INTERNAL ERROR
                    errorMsg = "خطا در پردازش رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-16";
                    break;
                case -17: // REVERS TRANSACTIN FROM OTHER BANK
                    errorMsg = "خطا در پردازش رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-17";
                    break;
                case -18: //INVALID IP
                    errorMsg = "خطا در پردازش رسید دیجیتالی در نتیجه خرید شما تایید نگریید" + "-18";
                    break;
            }

            return errorMsg;
        }
    }
}