Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports Telerik.Web.UI
Imports System.IO
Partial Public Class facturas_recibidas
    Inherits System.Web.UI.Page
    Private ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            calFechaInicio.SelectedDate = Date.Now.AddDays(-7)
            calFechaFin.SelectedDate = Date.Now

            Dim ObjData As New DataControl
            ObjData.Catalogo(proveedorid, "select id, razonsocial as nombre from tblMisProveedores order by razonsocial", 0)
            ObjData = Nothing
        End If
    End Sub

    Private Sub MuestraLista()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pFacturaProveedor @cmd=6 , @fechaini='" & calFechaInicio.SelectedDate.Value.ToShortDateString & "', @fechafin='" & calFechaFin.SelectedDate.Value.ToShortDateString & "', @proveedorid='" & proveedorid.SelectedValue.ToString & "'")
        facturaslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros."
        facturaslist.DataSource = ds
        facturaslist.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Protected Sub facturaslist_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles facturaslist.ItemCommand
        Select Case e.CommandName
            Case "cmdXML"
                DownloadXML(e.CommandArgument)
            Case "cmdPDF"
                DownloadPDF(e.CommandArgument)
        End Select
    End Sub

    Protected Sub facturaslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles facturaslist.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Facturas" Then

                Dim lblXML As Label = CType(dataItem("XML").FindControl("lblXML"), Label)
                Dim lblPDF As Label = CType(dataItem("XML").FindControl("lblPDF"), Label)
                Dim lnkXML As LinkButton = CType(dataItem("XML").FindControl("lnkXML"), LinkButton)
                Dim lnkPDF As LinkButton = CType(dataItem("XML").FindControl("lnkPDF"), LinkButton)

                If lblXML.Text = "" Then
                    lnkXML.Text = ""
                End If

                If lblPDF.Text = "" Then
                    lnkPDF.Text = ""
                End If

            End If

        End If
    End Sub

    Protected Sub facturaslist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles facturaslist.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pFacturaProveedor @cmd=6 , @fechaini='" & calFechaInicio.SelectedDate.Value.ToShortDateString & "', @fechafin='" & calFechaFin.SelectedDate.Value.ToShortDateString & "', @proveedorid='" & proveedorid.SelectedValue.ToString & "'")
        facturaslist.DataSource = ds
        facturaslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros."
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        ''
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Call MuestraLista()
    End Sub

    Private Sub DownloadXML(ByVal id As Long)
        Dim xml As String = ""
        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand("exec pFacturaProveedor @cmd=4, @id='" & id.ToString & "'", connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                xml = rs("xml").ToString
            End If
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        Dim FilePath = Server.MapPath("~/portalcfd/proveedores/xml/") & xml.ToString
        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        End If
        ''
    End Sub

    Private Sub DownloadPDF(ByVal id As Long)
        Dim pdf As String = ""
        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand("exec pFacturaProveedor @cmd=4, @id='" & id.ToString & "'", connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                pdf = rs("pdf").ToString
            End If
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        Dim FilePath = Server.MapPath("~/portalcfd/proveedores/pdf/") & pdf.ToString
        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        End If
        ''
    End Sub

End Class