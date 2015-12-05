using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CustomAuth.Infrastructure;

namespace CustomAuth
{
    public class MvcApplication : System.Web.HttpApplication
    {
        
        //Методы Application_Start и Application_End — это специальные методы, 
        //не представляющие события HttpApplication. ASP.NET вызывает их 
        //один раз за время жизни домена приложения не для каждого экземпляра HttpApplication.
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyResolver.SetResolver(new NinjectDependencyResolver());
        }


        //IIS при обработке запроса пропускает его внутри рабочего процесса через 
        //последовательность специальных компонент – модулей. Например фильтрация,
        //перенаправление, кэширование, аутентификация, авторизация. Каждый такой 
        //модуль ассоциируется с определённым событием, а их последовательность
        //составляют событийную модель обработки запросов. 
        //http://habrahabr.ru/post/189086/
        //https://msdn.microsoft.com/ru-ru/library/ms178473(v=vs.100).aspx

        //ASP.NET автоматически привязывает события приложения к обработчикам
        //в файле Global.asax с помощью соглашения об именах Application_событие,
        //например, Application_BeginRequest. 
        protected void Application_EndRequest()
        {
            
        }

        #region Overrides of HttpApplication

        //Вызывается один раз для каждого экземпляра класса 
        //HttpApplication после создания всех модулей.
        public override void Init()
        {
            //base.Init();
        }
        //Жизненный цикл приложения ASP.NET может расширяться через классы IHttpModule.
        //ASP.NET включает несколько классов, реализующих IHttpModule (FormsAuthenticationModule),
        //можно также создать собственный класс, реализующий IHttpModule.

        //При добавлении к приложению модулей, они сами могут создавать события.
        //Приложение может выполнить подписку на эти события в файле Global.asax
        //с помощью соглашения ИмяМодуля_ИмяСобытия. 
        
        //Например, для обработки события
        //Authenticate, созданного объектом FormsAuthenticationModule, можно
        //создать обработчик с именем FormsAuthentication_Authenticate.
        #endregion

        //1. Модуль FormsAuthenticationModule предоставляет событие Authenticate,
        //которое дает возможность обеспечить пользовательский объект IPrincipal 
        //(контекст безопасности, который представляет удостоверение пользователя
        // IIdentity для текущего запроса) для свойства User текущего контекста
        //HttpContext. 

        //2. Событие Authenticate возникает во время события AuthenticateRequest.

        //3. Для события Authenticate класса FormsAuthenticationModule 
        //определен делегат FormsAuthenticationEventHandler с агрументом 
        //FormsAuthenticationEventArgs 

        //4. Доступ к событию Authenticate осуществляется указанием подпрограммы
        //с именем FormsAuthentication_OnAuthenticate в файле Global.asax для
        //пользовательского приложения ASP.NET.

        //5. Событие FormsAuthentication_OnAuthenticate вызывается только в 
        //том случае, когда режим проверки подлинности имеет значение Forms 
        //в элементе authentication Element (ASP.NET Settings Schema) файла 
        //конфигурации приложения, а FormsAuthenticationModule является активным
        //модулем HTTP для данного приложения.

    }
}
