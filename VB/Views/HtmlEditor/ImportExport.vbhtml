@ModelType VB.HtmlEditorModel
    
<p>
    Sample document:
    @Html.DevExpress().HyperLink( _
        Sub(settings)
                settings.Name = "lnkDocument"
                settings.Properties.Text = ViewData("SampleDocumentPath").ToString()
                settings.NavigateUrl = settings.Properties.Text
        End Sub).GetHtml()
</p>
@Html.DevExpress().Button( _
        Sub(settings)
                settings.Name = "btnImport"
                settings.RouteValues = New With {Key .Controller = "HtmlEditor", Key .Action = "ImportSampleDocument"}
                settings.Text = "Import the document"
        End Sub).GetHtml()
<br />
@Using (Html.BeginForm("ExportContentTo", "HtmlEditor"))
    @Html.Partial("ImportExportPartial", Model)
    @<br />
    @Html.DevExpress().Button( _
        Sub(settings)
            settings.Name = "btnExport"
            settings.UseSubmitBehavior = True
            settings.Text = "Export content to"
        End Sub).GetHtml()
    @<br />
    @Html.DevExpress().ComboBox( _
        Sub(settings)
            settings.Name = "cmbFormat"
            settings.Properties.ValueType = GetType(HtmlEditorExportFormat)
        End Sub).BindList(System.Enum.GetNames(GetType(HtmlEditorExportFormat))).Bind(HtmlEditorExportFormat.Rtf).GetHtml()
End Using