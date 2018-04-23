Imports System
Imports System.Collections.Generic
Imports System.Web.UI.WebControls
Imports System.Web.Mvc
Imports DevExpress.Web.Mvc
Imports DevExpress.Web.ASPxHtmlEditor

Public Class HtmlEditorController
    Inherits System.Web.Mvc.Controller

    Private Const SampleDocumentPath As String = "~/Content/HtmlEditor/SampleImportDocument.rtf"

    Public Function ImportExport() As ActionResult
        ViewData("SampleDocumentPath") = SampleDocumentPath
        Dim htmlContentPath As String = Server.MapPath("~/Content/HtmlEditor/ImportExport.htm")
        Dim html As String = System.IO.File.ReadAllText(htmlContentPath)
        'ViewData("HTML") = html
        Return View("ImportExport", New HtmlEditorModel(html))
    End Function

    Public Function ImportExportPartial() As ActionResult
        Return PartialView("ImportExportPartial")
    End Function

    Public Function ExportTo(ByVal format As HtmlEditorExportFormat) As ActionResult
        Return HtmlEditorExtension.Export(HtmlEditorDemosHelper.SetHtmlEditorExportSettings(New HtmlEditorSettings()), format)
    End Function

    '
    <HttpPost()> _
    Public Function ExportContentTo() As ActionResult
        Dim format As HtmlEditorExportFormat = EditorExtension.GetValue(Of HtmlEditorExportFormat)("cmbFormat")

        Dim stream As New System.IO.MemoryStream()
        HtmlEditorExtension.Export(HtmlEditorDemosHelper.SetHtmlEditorExportSettings(New HtmlEditorSettings()), stream, format)

        Dim fileFormat As String = format.ToString().ToLower().TrimStart("."c)

        Dim result As New FileStreamResult(stream, "application/" & fileFormat)
        result.FileDownloadName = String.Format("{0}.{1}", DateTime.Now.ToShortDateString().Replace(".", "_"), fileFormat)
        result.FileStream.Position = 0

        Return result
    End Function
    '

    Public Function ImportSampleDocument() As ActionResult
        ViewData("SampleDocumentPath") = SampleDocumentPath
        Dim model As HtmlEditorModel = Nothing

        HtmlEditorExtension.Import(
            "~/Content/HtmlEditor/SampleImportDocument.rtf",
            HtmlEditorDemosHelper.ImportContentDirectory,
            Sub(html, cssFiles)
                model = New HtmlEditorModel(html, cssFiles)
                'ViewData("HTML") = html
            End Sub
        )

        Return View("ImportExport", model)
    End Function
End Class

Public Class HtmlEditorModel
    Public Sub New(ByVal html As String)
        Me.New(html, Nothing)
    End Sub
    Public Sub New(ByVal html As String, ByVal cssFiles As IEnumerable(Of String))
        Me.html = html
        Me.cssFiles = cssFiles
    End Sub

    Private privateHtml As String
    Public Property Html() As String
        Get
            Return privateHtml
        End Get
        Set(ByVal value As String)
            privateHtml = value
        End Set
    End Property
    Private privateCssFiles As IEnumerable(Of String)
    Public Property CssFiles() As IEnumerable(Of String)
        Get
            Return privateCssFiles
        End Get
        Set(ByVal value As IEnumerable(Of String))
            privateCssFiles = value
        End Set
    End Property
End Class

Public Class HtmlEditorDemosHelper
    Public Const ImportContentDirectory As String = "~/Content/HtmlEditor/Imported"

    Public Shared Function SetHtmlEditorExportSettings(ByVal settings As HtmlEditorSettings) As HtmlEditorSettings
        settings.Name = "heImportExport"
        settings.CallbackRouteValues = New With {Key .Controller = "HtmlEditor", Key .Action = "ImportExportPartial"}
        settings.ExportRouteValues = New With {Key .Controller = "HtmlEditor", Key .Action = "ExportTo"}

        Dim toolbar = settings.Toolbars.Add()
        toolbar.Items.Add(New ToolbarUndoButton())
        toolbar.Items.Add(New ToolbarRedoButton())
        toolbar.Items.Add(New ToolbarBoldButton(True))
        toolbar.Items.Add(New ToolbarItalicButton())
        toolbar.Items.Add(New ToolbarUnderlineButton())
        toolbar.Items.Add(New ToolbarStrikethroughButton())
        toolbar.Items.Add(New ToolbarInsertImageDialogButton(True))
        Dim saveButton As New ToolbarExportDropDownButton(True)
        saveButton.CreateDefaultItems()
        toolbar.Items.Add(saveButton)

        settings.PreRender = _
            Sub(sender, e)
                Dim editor As ASPxHtmlEditor = CType(sender, ASPxHtmlEditor)
                editor.Width = Unit.Percentage(100)
            End Sub

        Return settings

    End Function

End Class