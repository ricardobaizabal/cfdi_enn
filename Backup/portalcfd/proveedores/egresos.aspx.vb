Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Threading
Imports System.Globalization
Imports Telerik.Web.UI
Partial Public Class egresos
    Inherits System.Web.UI.Page

    Private ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fechaini.SelectedDate = Date.Now.AddDays(-7)
            fechafin.SelectedDate = Date.Now
            cuentaid.Enabled = False
            Dim ObjData As New DataControl
            ObjData.Catalogo(proveedorid, "select id, razonsocial as nombre from tblMisProveedores order by razonsocial", 0)
            ObjData.Catalogo(bancoid, "select id,nombre from tblBanco order by nombre", 0)
            ObjData.Catalogo(cuentaid, "select id, cuenta from tblCuentasBanco where bancoid='" & bancoid.SelectedValue.ToString & "'", 0)
            ObjData = Nothing
        End If
    End Sub

    Protected Sub facturaslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles facturaslist.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Footer
                If ds.Tables(0).Rows.Count > 0 Then
                    '
                    'If Not IsDBNull(ds.Tables(0).Compute("sum(total_dolares)", "estatus<>'Cancelada'")) Then
                    '    e.Item.Cells(10).Text = FormatCurrency(ds.Tables(0).Compute("sum(total_dolares)", "estatus<>'Cancelada'"), 2).ToString
                    '    e.Item.Cells(10).HorizontalAlign = HorizontalAlign.Right
                    '    e.Item.Cells(10).Font.Bold = True
                    'End If
                    '
                    If Not IsDBNull(ds.Tables(0).Compute("sum(total_pesos)", "estatus<>'Cancelada'")) Then
                        e.Item.Cells(10).Text = FormatCurrency(ds.Tables(0).Compute("sum(total_pesos)", "estatus<>'Cancelada'"), 2).ToString
                        e.Item.Cells(10).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(10).Font.Bold = True
                    End If
                    '
                End If
        End Select
    End Sub

    Protected Sub facturaslist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles facturaslist.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pCuentasPorPagar @cmd=6, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @proveedorid='" & proveedorid.SelectedValue.ToString & "'")
        facturaslist.DataSource = ds
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pCuentasPorPagar @cmd=6, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @proveedorid='" & proveedorid.SelectedValue.ToString & "', @bancoid='" & bancoid.SelectedValue.ToString & "', @cuentaid='" & cuentaid.SelectedValue.ToString & "', @monedaid='" & monedaid.SelectedValue.ToString & "'")
        facturaslist.DataSource = ds
        facturaslist.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Protected Sub bancoid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles bancoid.SelectedIndexChanged
        Dim ObjData As New DataControl
        ObjData.Catalogo(cuentaid, "select id, cuenta from tblCuentasBanco where bancoid='" & bancoid.SelectedValue.ToString & "'", 0)
        ObjData = Nothing
        If bancoid.SelectedValue <> 0 Then
            cuentaid.Enabled = True
        Else
            cuentaid.Enabled = False
        End If
    End Sub

End Class