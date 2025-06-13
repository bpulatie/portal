<%@ Application Language="C#" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="log4net" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        log4net.Config.XmlConfigurator.Configure();
        ILog logger = log4net.LogManager.GetLogger("SPALog");
        logger.Info("Website Startup.");
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
        ILog logger = log4net.LogManager.GetLogger("SPALog");
        logger.Info("Website Shutdown.");
    }

    void Application_Error(object sender, EventArgs e)
    {
        ILog logger = log4net.LogManager.GetLogger("SPALog");

        // Code that runs when an unhandled error occurs

        // Get the exception object.
        Exception exc = Server.GetLastError();

        // Handle HTTP errors
        if (exc.GetType() == typeof(HttpException))
        {
            // The Complete Error Handling Example generates
            // some errors using URLs with "NoCatch" in them;
            // ignore these here to simulate what would happen
            // if a global.asax handler were not implemented.
            if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                return;

            //Redirect HTTP errors to HttpError page
            // Server.Transfer("default.htm");
        }

        if (!exc.Message.Contains("cannot clear headers"))
            logger.Error("Global Error Page: " + exc.Message);

        // Clear the error from the server
        Server.ClearError();
    }

    void Application_BeginRequest(object sender, EventArgs e)
    {
        //Response.BufferOutput = true;    
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
