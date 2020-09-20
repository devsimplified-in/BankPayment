﻿using System.Collections.Generic;

namespace Adin.BankPayment.Parsian
{
    public static class ParsianErrors
    {
        public static (bool Success, string Message) GetResult(int status)
        {
            return (status == 0, ErrorsDictionary[status]);
        }

        private static readonly Dictionary<int, string> ErrorsDictionary = new Dictionary<int, string>()
        {
            {93, "امکان تکمیل تراکنش وجود ندارد."},
            {92, "مقصد تراکنش پیدا نشد."},
            {91, "سیستم صدور مجوز انجام تراکنش موقتا غیر فعال است و یا زمان تعیین شده برای صدور مجوز به پایان رسیده است."},
            {84, "در تراکنش هایی که انجام آن مستلزم ارتباط با صادر کننده است در صورت فعال نبودن صادرکننده این پیام در پاسخ ارسال خواهد شد."},
            {83, "سرویس دهنده سوئیچ کارت تراکنش را نپذیرفته است."},
            {81, "کارت پذیرفته نشد."},
            {80, "درخواست تراکنش رد شده است."},
            {79, "حساب متصل به کارت نامعتبر است یا دارای اشکال است."},
            {78, "کارت فعال نیست."},
            {75, "تعداد دفعات ورود رمز غلط بیش از حد مجاز است."},
            {69, "تعداد دفعات تکرار رمز از حد مجاز گذشته است."},
            {68, "پایخ لازم برای تکمیل یا انجام تراکنش خیلی دیر رسیده است."},
            {65, "تعداد درخواست تراکنش بیش از حد مجاز است."},
            {63, "تمهیدات امنیتی نقض گردیده است."},
            {62, "کارت محدود شده است."},
            {61, "مبلغ تراکنش بیش از حذ مجاز است."},
            {59, "کارت مظنون به تقلب است."},
            {58, "انجام تراکنش مربوطه توسط پایانه ی انجام دهنده مجاز نیست."},
            {57, "انجام تراکنش مربوطه توسط دارنده کارت مجاز نیست."},
            {56, "کارت نامعتبر است."},
            {55, "رمز کارت نامعتبر است."},
            {54, "تاریخ انقضای کارت سپری شده است."},
            {51, "موجودی کافی نیست."},
            {45, "قبض قابل پرداخت نیست."},
            {43, "کارت مسروقه است."},
            {41, "کارت مفقودی است."},
            {40, "عملیات درخواستی پشتیبانی نمی گردد."},
            {39, "کارت حساب اعتباری ندارد."},
            {38, "تعداد دفعات ورود رمز غلط بیش از حد مجاز است. کارت توسط دستگاه ضبط شود."},
            {33, "تاریخ انقضای کارت سپری شده است."},
            {32, "تراکنش به صورت غیر قطعی کامل شده است."},
            {31, "پذیرنده توسط سوئیچ پشتیبانی نمی شود."},
            {30, "قالب پیام دارای اشکال است."},
            {22, "تراکنش مشکوک به بد عمل کردن(کارت، ترمینال، دارنده کارت) بوده است لذا پذیرفته نشده است."},
            {21, "در صورتی که پاسخ به درخواست ترمینال نیازمند هیچ پاسخ خاص یا عملکردی نباشیم این پیام را خواهیم داشت."},
            {20, "در موقعیتی که سوئیچ جهت پذیرش تراکنش نیازمند پرس و جو از کارت است ممکن است درخواست از کارت بنماید این پیام مبین نامعتبر بودن جواب است."},
            {17, "مشتری درخواست کننده حذف شده است."},
            {15, "صادرکننده ی کارت نامعتبر است.(وجود ندارد)"},
            {14, "شماره کارت ارسالی نامعتبر است.(وجود ندارد)"},
            {13, "مبلغ تراکنش نادرست است."},
            {12, "تراکنش نامعتبر است."},
            {10, "تراکنش با مبلغی کمتر از مبلغ درخواستی(کمبود حساب مشتری)پذیرفته شده است."},
            {9, "درخواست رسیده در حال پیگیری و انجام است."},
            {8, "با تشخیص هویت دارنده یکارت، تراکنش موفق است."},
            {6, "بروز خطایی ناشناخته."},
            {5, "از انجام تراکنش صرف نظر شد."},
            {3, "پذیرنده ی فروشگاهی نامعتبر است."},
            {2, "عملیات تاییدیه این تراکنش قبلا با موفقیت صورت پذیرفته است."},
            {1, "صادر کننده کارت از انجام تراکنش صرف نظر کرد."},
            {0, "عملیات با موفقیت انجام شد."},
            {-1, "خطای سرور"},
            {-2, "درخواست نا معتبر است."},
            {-3, "تراکنشی یافت نشد."},
            {-4, "پاسخ دریافتی از بانک نامعتبر است."},
            {-5, "مبلغ پرداخت شده معتبر نیست."},
            {-6, "تراکنش توسط خریدار لغو شده است."},
            {-7, "خطای تعریف نشده"},
            {-100, "پذیرنده غیر فعال است."},
            {-101, "پذیرنده احراز هویت نشد."},
            {-102, "تراکنش با موفقیت برگشت داده شد."},
            {-103, "قابلیت خرید برای پذیرنده غیرفعال است."},
            {-104, "قابلیت گرداخت قبض برا پذیرنده غیرفعال است."},
            {-105, "قابلیت تاپ آپ برای پذیرنده غیرفعال است."},
            {-106, "قابلیت شارژ برای پذیرنده غیرفعال است."},
            {-107, "قابلیت ارسال تاییدیه تراکنش برای پذیرنده غیرفعال است."},
            {-108, "قابلیت برگشت تراکنش برای پذیرنده غیرفعال است."},
            {-111, "مبلغ تراکنش بیشت از حد مجاز پذیرنده است."},
            {-112, "شماره سفارش تکراری است."},
            {-113, "پارامتر ورودی خالی است."},
            {-114, "شناسه قبض نامعتبر است."},
            {-115, "شناسه پرداخت نامعتبر است."},
            {-116, "طول رشته بیش از حد مجاز است."},
            {-117, "طول رشته کمتر از حد مجاز است."},
            {-118, "مقدار ارسال شده عدد نیست"},
            {-119, "سازمان نامعتبر است."},
            {-120, "طول داده ورودی معتبر نیست."},
            {-121, "رشته داده شده بطور کامل عددی نیست."},
            {-126, "کد شناسایی پذیرنده معتبر نیست."},
            {-127, "آدرس اینترنتی معتبر نیست."},
            {-128, "قالب آدرس IP معتبر نیست."},
            {-130, "زمان Token منقضی شده است."},
            {-131, "Token نامعتبر است."},
            {-132, "مبلغ تراکنش کمتر از حداقل مجاز است."},
            {-138, "عملیات پرداخت توسط کاربر لغو شد."},
            {-1505, "تایید تراکنش توسط پذیرنده انجام شد."},
            {-1507, "تراکنش برگشت به سوئیچ ارسال شد."},
            {-1527, "انجام عمیلیات درخواست پرداخت تراکنش خرید نامفق بود."},
            {-1528, "اطلاعات پرداخت یافت نشد."},
            {-1530, "پذیرنده مجاز به تایید این تراکنش نیست."},
            {-1531, "تایید تراکنش نامفق امکان پذیر نیست."},
            {-1532, "تراکنش از سوی پذیرنده تایید شد."},
            {-1533, "تراکنش قبلا تایید شده است"},
            {-1536, "تراکنش قبلا تایید شده است"},
            {-1540, "تایید تراکنش ناموفق است."},
            {-1548, "فارخوانی سرویس درخواست پرداخت قبض ناموفق بود."},
            {-1549, "زمان مجاز برای درخواست برگشت تراکنش به اتمام رسیده است."},
            {-1550, "برگشت تراکنش در وضعیت جاری امکان پذیر نیست."},
            {-1551, "برگشت تراکنش قبلا انجام شده است."},
            {-1552, "برگشت تراکنش مجاز نیست."},
            {-32768, "خطای ناشناخته رخ داده است."}
        };
    }
}