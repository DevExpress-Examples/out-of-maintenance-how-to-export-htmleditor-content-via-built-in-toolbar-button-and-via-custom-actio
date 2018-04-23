<!DOCTYPE HTML>
<html>
<head>
    <title></title>
    @Html.DevExpress().GetStyleSheets(New StyleSheet With {.ExtensionSuite = ExtensionSuite.GridView}, New StyleSheet With {.ExtensionSuite = ExtensionSuite.HtmlEditor}, New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors}, New StyleSheet With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout})
    <link href="@Url.Content("~/Content/Site.css")" rel="stylesheet" type="text/css" />
    @Html.DevExpress().GetScripts(New Script With {.ExtensionSuite = ExtensionSuite.GridView}, New Script With {.ExtensionSuite = ExtensionSuite.HtmlEditor}, New Script With {.ExtensionSuite = ExtensionSuite.Editors}, New Script With {.ExtensionSuite = ExtensionSuite.NavigationAndLayout})
</head>
<body>
    @RenderBody()
</body>
</html>