using JobPlatformBackend.Business.src.Services.Abstractions;
using JobPlatformBackend.Domain.src.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatformBackend.Business.src.Services.Implementations
{
	public class EmailTemplateService : IEmailTemplateService
	{
		public string GetResetPasswordEmail(string otp)
		{
			return $@"
<div dir='rtl' style='font-family: sans-serif; max-width: 500px; margin: 20px auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 12px; text-align: center; background-color: #ffffff;'>
    <div style='background-color: #34495e; padding: 15px; border-radius: 8px; margin-bottom: 20px;'>
        <h2 style='color: white; margin: 0;'>إعادة تعيين كلمة المرور</h2>
    </div>
    <p style='color: #555; font-size: 16px;'>لقد تلقينا طلباً لتغيير كلمة المرور الخاصة بحسابك في دروب.</p>
    <p style='color: #7f8c8d;'>أدخل الرمز التالي في التطبيق للمتابعة:</p>
    <div style='background-color: #fdf2e9; border: 1px solid #e67e22; padding: 20px; margin: 25px 0; border-radius: 10px;'>
        <span style='font-size: 36px; font-weight: bold; color: #d35400; letter-spacing: 10px;'>{otp}</span>
    </div>
    <p style='color: #7f8c8d; font-size: 14px;'>تنتهي صلاحية هذا الرمز بعد <b>5 دقائق</b>.</p>
    <div style='margin-top: 25px; padding: 10px; background-color: #fff5f5; border-radius: 5px;'>
        <p style='color: #c0392b; font-size: 12px; margin: 0;'>لأسباب أمنية: لا تشارك هذا الرمز مع أي شخص مهما كان.</p>
    </div>
</div>";
		}

		public string GetVerificationEmail(string otp)
		{
return $@"
<div dir='rtl' style='background-color: #0f172a; padding: 20px; font-family: ""Segoe UI"", Tahoma, sans-serif;'>
    <table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px; background-color: #ffffff; border-radius: 24px; overflow: hidden; box-shadow: 0 20px 40px rgba(0,0,0,0.4);'>
        
        <tr>
            <td align='center' style='padding: 40px 20px; background: linear-gradient(135deg, #1e3a8a 0%, #3b82f6 100%);'>
                <div style='background: rgba(255, 255, 255, 0.2); width: 70px; height: 70px; border-radius: 18px; margin-bottom: 15px; display: inline-block;'>
                    <span style='font-size: 35px; line-height: 70px;'>🚀</span>
                </div>
                <h1 style='color: #ffffff; margin: 0; font-size: 26px; font-weight: bold;'>منصة دروب</h1>
                <p style='color: #dbeafe; margin: 8px 0 0; font-size: 14px;'>بوابتك الموثوقة لعالم الوظائف</p>
            </td>
        </tr>

        <tr>
            <td style='padding: 40px 30px;'>
                <h2 style='color: #1e293b; font-size: 22px; text-align: center; margin-bottom: 15px;'>رمز التحقق الخاص بك</h2>
                <p style='color: #64748b; font-size: 15px; line-height: 1.6; text-align: center;'>
                    أهلاً بك! استخدم الرمز أدناه لتفعيل حسابك والبدء في بناء سيرتك الذاتية الاحترافية.
                </p>

                <div style='margin: 30px 0; background-color: #f8fafc; border: 2px dashed #3b82f6; border-radius: 16px; padding: 25px; text-align: center;'>
                    <div style='font-size: 45px; font-weight: 800; color: #1e3a8a; letter-spacing: 12px; font-family: monospace;'>
                        {otp}
                    </div>
                </div>

                <p style='color: #ef4444; font-size: 13px; text-align: center; font-weight: bold;'>
                    ⏱️ صالح لمدة 5 دقائق فقط
                </p>
            </td>
        </tr>

        <tr>
            <td style='padding: 0 30px 30px;'>
                <div style='background-color: #f0f7ff; border-radius: 12px; padding: 15px; border-right: 4px solid #3b82f6;'>
                    <p style='margin: 0; color: #1e40af; font-size: 13px; line-height: 1.5;'>
                        <b>نصيحة مهنية:</b> الشركات تنجذب للملفات التي تحتوي على صورة شخصية احترافية ووصف وظيفي دقيق.
                    </p>
                </div>
            </td>
        </tr>

        <tr>
            <td style='background-color: #f1f5f9; padding: 20px; text-align: center; color: #94a3b8; font-size: 11px;'>
                تم إرسال هذا البريد آلياً من منصة دروب للتوظيف 2026<br>
                إذا لم تطلب هذا الرمز، يرجى تجاهل الرسالة.
            </td>
        </tr>
    </table>
</div>";
		}

		public string GetWelcomeEmail(string userName)
		{
			return $@"
<div dir='rtl' style='font-family: ""Segoe UI"", Tahoma, Geneva, Verdana, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #eee; border-radius: 15px; overflow: hidden; background-color: #f9f9f9;'>
    
    <div style='background: linear-gradient(135deg, #2c3e50, #2980b9); padding: 30px; text-align: center; color: white;'>
        <h1 style='margin: 0; font-size: 28px;'>دروب | Doroob</h1>
        <p style='margin-top: 10px; opacity: 0.9;'>بوابتك نحو المستقبل المهني</p>
    </div>

    <div style='padding: 40px; background-color: white; text-align: center;'>
        <h2 style='color: #2c3e50; margin-bottom: 20px;'>سعدنا بعودتك، {userName}! 👋</h2>
        <p style='color: #7f8c8d; line-height: 1.6; font-size: 16px;'>
            اشتقنا لتواجدك معنا. هناك العديد من الفرص الوظيفية الجديدة التي بانتظارك اليوم. هل أنت مستعد للخطوة التالية في مسيرتك؟
        </p>
        
        <div style='margin-top: 30px;'>
            <a href='https://doroob.sa/dashboard' style='background-color: #2980b9; color: white; padding: 15px 35px; text-decoration: none; border-radius: 25px; font-weight: bold; display: inline-block; transition: background 0.3s;'>
                تصفح الوظائف الآن
            </a>
        </div>
    </div>

    <div style='padding: 20px; text-align: center; background-color: #f1f1f1; color: #95a5a6; font-size: 12px;'>
        <p>© 2026 منصة دروب للوظائف. جميع الحقوق محفوظة.</p>
        <p>وصلك هذا البريد لأنك مسجل في منصتنا.</p>
    </div>
</div>";
		}
	}
}
