<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {               
        RegisterRoutes(RouteTable.Routes);
    }

    void RegisterRoutes(RouteCollection routes)
    {
        routes.Ignore("{resource}.axd/{*pathInfo}");

        routes.MapPageRoute("huabaoShow", "huabao/show/{sID}/{*extrainfo}", "~/huabao/show.aspx", true);
        routes.MapPageRoute("goodsShow", "show/{sID}/{*extrainfo}", "~/item/show.aspx", true);

        routes.MapPageRoute("huabaoList", "huabao/{cID}/{*extrainfo}", "~/huabao/list.aspx", true);
        routes.MapPageRoute("goodsList", "{cID}/{*extrainfo}", "~/item/default.aspx", true);

        routes.MapPageRoute("error", "{dir}/{*extrainfo}", "~/include/error.aspx", true);
    }   

</script>