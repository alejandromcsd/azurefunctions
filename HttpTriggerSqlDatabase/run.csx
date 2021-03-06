using System.Net;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    var successful =true;
    try
    {
        var cnnString  = ConfigurationManager.ConnectionStrings["MS_TableConnectionString"].ConnectionString;
        
        using(var connection = new SqlConnection(cnnString))
        {
            connection.Open();
            
            var rLog = await req.Content.ReadAsAsync<LogRequest>();
            
            // insert a log to the database
            connection.Execute("INSERT INTO [dbo].[LogRequest] ([Log]) VALUES (@Log)", rLog);
            log.Info("Log added to database successfully VCODE MAX IS NOT PUTONA 2222!");
        }
    }
    catch(Exception ex)
    {
        log.Info(ex.ToString());
        successful=false;
    }
    
    return !successful
        ? req.CreateResponse(HttpStatusCode.BadRequest, "Unable to process your request!")
        : req.CreateResponse(HttpStatusCode.OK, "Data saved successfully!");
}
public class LogRequest
{
    public int Id{get;set;}
    public string Log{get;set;}
}