Imports System.Data
Imports System.Threading
Imports System.Globalization
Partial Class portalcfd_reportes_ingresos
    Inherits System.Web.UI.Page
    Private ds As DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle
        lblReportsLegend.Text = Resources.Resource.lblReportsLegend & " - Reporte de ingresos"
        If Not IsPostBack Then
            fechaini.SelectedDate = DateAdd(DateInterval.Day, -7, Now)
            fechafin.SelectedDate = Now
            Dim Objdata As New DataControl
            Objdata.Catalogo(clienteid, "exec pCatalogos @cmd=2", 0)
            Objdata = Nothing
            Call MuestraReporte()
        End If
    End Sub
    Private Sub MuestraReporte()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pMisInformes @cmd=9, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @clienteid='" & clienteid.SelectedValue.ToString & "'")
        reporteGrid.DataSource = ds
        reporteGrid.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub
    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Call MuestraReporte()
    End Sub

    Protected Sub reporteGrid_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles reporteGrid.ItemCommand
        Select Case e.CommandName
            Case "cmdFolio"
                Response.Redirect("~/portalcfd/CFD_Detalle.aspx?id=" & e.CommandArgument.ToString)
        End Select
    End Sub

    Protected Sub reporteGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles reporteGrid.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                If e.Item.DataItem("estatus_cobranza") = "Pendiente" Then
                    e.Item.Cells(e.Item.Cells.Count - 1).ForeColor = Drawing.Color.Blue
                Else
                    e.Item.Cells(e.Item.Cells.Count - 1).ForeColor = Drawing.Color.Green
                End If
                '
                If (e.Item.DataItem("estatus") = 3) Or (e.Item.DataItem("escancelada")) Then
                    e.Item.Cells(e.Item.Cells.Count - 1).Text = "Cancelada"
                    e.Item.Cells(e.Item.Cells.Count - 1).ForeColor = Drawing.Color.Red
                    e.Item.Cells(e.Item.Cells.Count - 1).Font.Bold = True
                    Dim lnkFolio As LinkButton = CType(e.Item.FindControl("lnkFolio"), LinkButton)
                    lnkFolio.Visible = False
                    Dim lblFolio As Label = CType(e.Item.FindControl("lblFolio"), Label)
                    lblFolio.Text = e.Item.DataItem("folio").ToString
                    lblFolio.Visible = True
                End If
                '
                e.Item.Cells(e.Item.Cells.Count - 1).Font.Bold = True
            Case Telerik.Web.UI.GridItemType.Footer
                If ds.Tables(0).Rows.Count > 0 Then
                    e.Item.Cells(7).Text = FormatCurrency(ds.Tables(0).Compute("sum(importe)", "escancelada=0"), 2).ToString
                    e.Item.Cells(7).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(7).Font.Bold = True
                    '
                    e.Item.Cells(8).Text = FormatCurrency(ds.Tables(0).Compute("sum(iva)", "escancelada=0"), 2).ToString
                    e.Item.Cells(8).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(8).Font.Bold = True
                    '
                    e.Item.Cells(9).Text = FormatCurrency(ds.Tables(0).Compute("sum(total)", "escancelada=0"), 2).ToString
                    e.Item.Cells(9).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(9).Font.Bold = True
                End If
        End Select
    End Sub

    Protected Sub reporteGrid_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles reporteGrid.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pMisInformes @cmd=9, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "'")
        reporteGrid.DataSource = ds
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Protected Sub reporteGrid_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles reporteGrid.PageIndexChanged
        '
        reporteGrid.CurrentPageIndex = e.NewPageIndex
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pMisInformes @cmd=9, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @clienteid='" & clienteid.SelectedValue.ToString & "'")
        reporteGrid.DataSource = ds
        reporteGrid.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        ''
    End Sub
End Class
