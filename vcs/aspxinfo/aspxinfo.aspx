<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>aspxinfo</title>
</head>
<body>
    <style>
        h2 {
            color: Red;
            border-bottom-color: Red;
            border-bottom-style: solid;
            border-bottom-width: 3px;
        }

        h3 {
            color: blue;
            border-bottom-color: blue;
            border-bottom-style: solid;
        }

        dt {
            font-weight: bold;
            color: green;
        }
    </style>

    <h2>Server</h2>
    <div>
        <dl>
            <dt>MachineName</dt>
            <dd><%: Server.MachineName %></dd>
        </dl>
    </div>

    <h2>Request</h2>
    <div>
        <dl>
            <dt>Url</dt>
            <dd><%: Request.Url %></dd>
            <dt>HttpMethod</dt>
            <dd><%: Request.HttpMethod%></dd>
            <dt>Path</dt>
            <dd><%: Request.Path%></dd>
            <dt>QueryString</dt>
            <dd><%: Request.QueryString%></dd>
            <dt>UrlReferrer</dt>
            <dd><%: Request.UrlReferrer %></dd>
            <dt>UserAgent</dt>
            <dd><%: Request.UserAgent%></dd>
            <dt>UserHostAddress</dt>
            <dd><%: Request.UserHostAddress%></dd>
            <dt>UserHostName</dt>
            <dd><%: Request.UserHostName%></dd>
            <dt>UserLanguages</dt>
            <dd><%: Request.UserLanguages%></dd>
            <dt>RequestType</dt>
            <dd><%: Request.RequestType%></dd>
            <dt>RawUrl</dt>
            <dd><%: Request.RawUrl%></dd>
            <dt>PhysicalPath</dt>
            <dd><%: Request.PhysicalPath%></dd>
            <dt>PhysicalApplicationPath</dt>
            <dd><%: Request.PhysicalApplicationPath%></dd>
            <dt>PathInfo</dt>
            <dd><%: Request.PathInfo%></dd>
            <dt>FilePath</dt>
            <dd><%: Request.FilePath%></dd>
            <dt>ContentType</dt>
            <dd><%: Request.ContentType%></dd>
        </dl>
    </div>

    <h3>ServerVariables</h3>
    <div>
        <dl>
            <%
                Response.Write("\r\n");
                var servervars = Request.ServerVariables;
                foreach (string key in servervars)
                {
                    if (key == "ALL_RAW" || key == "ALL_HTTP")
                    {
                        continue;
                    }
                    Response.Write("<dt>" + Server.HtmlEncode(key) + "</dt>\r\n");
                    string[] values = servervars.GetValues(key);
                    foreach (var value in values)
                    {
                        Response.Write("<dd>" + Server.HtmlEncode(value) + "</dd>\r\n");
                    }
                }
            %>
        </dl>
    </div>

    <h3>Headers</h3>
    <div>
        <dl>
            <%
                Response.Write("\r\n");
                var headers = Request.Headers;
                foreach (string key in headers)
                {
                    Response.Write("<dt>" + Server.HtmlEncode(key) + "</dt>\r\n");
                    string[] values = headers.GetValues(key);
                    foreach (var value in values)
                    {
                        Response.Write("<dd>" + Server.HtmlEncode(value) + "</dd>\r\n");
                    }
                }
            %>
        </dl>
    </div>

    <h3>Redirect Test</h3>
    <form runat="server">
        <asp:TextBox ID="RedirectTest" runat="server" Text="https://www.google.co.jp" />
        <asp:Button runat="server" OnClick="OnRedirectTest" Text="Redirect" />
        <script runat="server">
            void OnRedirectTest(object sender, EventArgs e)
            {
                Response.Redirect(RedirectTest.Text);
            }
        </script>
    </form>

</body>
</html>
