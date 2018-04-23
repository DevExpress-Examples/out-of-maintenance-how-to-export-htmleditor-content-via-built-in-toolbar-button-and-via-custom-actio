@ModelType VB.HtmlEditorModel

@Html.DevExpress().HtmlEditor( _
    Sub(settings)
            VB.HtmlEditorDemosHelper.SetHtmlEditorExportSettings(settings)
            If Model IsNot Nothing Then
                settings.Html = Model.Html
                If Model.CssFiles IsNot Nothing Then
                    settings.CssFiles.Clear()
                    settings.CssFiles.AddRange(Model.CssFiles)
                End If
            End If
            'settings.Html = ViewData("HTML")
    End Sub).GetHtml()