Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Threading
Imports System.Globalization
Imports System.Xml
Imports Telerik.Web.UI

Partial Public Class gastos
    Inherits System.Web.UI.Page

    Private ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ordencompraid.Enabled = False
            Dim ObjData As New DataControl
            ObjData.Catalogo(proveedorid, "select id, razonsocial as nombre from tblMisProveedores order by nombre", 0)
            ObjData.Catalogo(ordencompraid, "select id, 'O.C.-' + convert(varchar,id) + ' - ' + convert(varchar(10), fecha,103) as nombre from tblOrdenCompra where proveedorid='" & proveedorid.SelectedValue.ToString & "' and isnull(facturaid,0)=0 and isnull(gastoid,0)=0 order by id", 0)
            ObjData = Nothing
        End If
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        If GastoID.Value = 0 Then
            Dim DataControl As New DataControl
            DataControl.RunSQLQuery("EXEC pGastos @cmd=1, @proveedorid='" & proveedorid.SelectedValue.ToString & "', @ordencompraid='" & ordencompraid.SelectedValue.ToString & "', @descripcion_gasto='" & txtDescGasto.Text & "', @monto='" & txtMonto.Text & "', @no_documento='" & txtNoDocumento.Text & "', @fecha_promesa_pago='" & calFechaPago.SelectedDate.Value.ToShortDateString & "'")
            DataControl = Nothing
        Else
            Dim DataControl As New DataControl
            DataControl.RunSQLQuery("EXEC pGastos @cmd=4, @id='" & GastoID.Value & "', @proveedorid='" & proveedorid.SelectedValue.ToString & "', @descripcion_gasto='" & txtDescGasto.Text & "', @monto='" & txtMonto.Text & "', @no_documento='" & txtNoDocumento.Text & "', @fecha_promesa_pago='" & calFechaPago.SelectedDate.Value.ToShortDateString & "'")
            DataControl = Nothing
            GastoID.Value = 0
        End If

        GastoID.Value = 0
        proveedorid.SelectedValue = 0
        txtDescGasto.Text = ""
        txtMonto.Text = 0
        txtNoDocumento.Text = ""

        Call MuestraLista()

    End Sub

    Private Sub MuestraLista()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pGastos @cmd=2")
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
            Case "cmdEliminar"
                EliminaGasto(e.CommandArgument)
            Case "cmdVer"
                VerDocumento(e.CommandArgument)
        End Select
    End Sub

    Private Sub EliminaGasto(ByVal id As Integer)
        Dim DataControl As New DataControl
        DataControl.RunSQLQuery("EXEC pGastos @cmd=5, @id='" & id & "'")
        DataControl = Nothing
        Call MuestraLista()
    End Sub

    Protected Sub facturaslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles facturaslist.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Gastos" Then

                Dim lnkEliminar As LinkButton = CType(dataItem("Eliminar").FindControl("lnkEliminar"), LinkButton)
                lnkEliminar.Attributes.Add("onclick", "return confirm ('¿Esta seguro que desea eliminar este gasto?');")

                'Dim lnkEditar As LinkButton = CType(dataItem("Editar").FindControl("lnkEditar"), LinkButton)
                'lnkEditar.Attributes.Add("onclick", "javascript: openRadWindow('" & e.Item.DataItem("id").ToString & "'); return false;")

            End If

        End If
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Footer
                If ds.Tables(0).Rows.Count > 0 Then
                    '
                    If Not IsDBNull(ds.Tables(0).Compute("sum(monto)", "")) Then
                        e.Item.Cells(5).Text = FormatCurrency(ds.Tables(0).Compute("sum(monto)", ""), 2).ToString
                        e.Item.Cells(5).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(5).Font.Bold = True
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
        ds = ObjData.FillDataSet("exec pGastos @cmd=2")
        facturaslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros."
        facturaslist.DataSource = ds
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        ''
    End Sub

    Private Sub VerDocumento(ByVal id As Long)
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pGastos @cmd=3, @id='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                GastoID.Value = id
                proveedorid.SelectedValue = rs("proveedorid")
                txtDescGasto.Text = rs("descripcion_gasto")
                txtMonto.Text = rs("monto")
                txtNoDocumento.Text = rs("no_documento")
                If rs("fecha_promesa_pago").ToString.Trim.Length > 0 Then
                    calFechaPago.SelectedDate = CDate(rs("fecha_promesa_pago"))
                End If
            End If

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        GastoID.Value = 0
        proveedorid.SelectedValue = 0
        txtDescGasto.Text = ""
        txtMonto.Text = 0
        txtNoDocumento.Text = ""
    End Sub

    Private Sub proveedorid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles proveedorid.SelectedIndexChanged
        If proveedorid.SelectedValue = 0 Then
            ordencompraid.SelectedValue = 0
            ordencompraid.Enabled = False
        Else
            ordencompraid.Enabled = True
            Dim ObjData As New DataControl
            ObjData.Catalogo(ordencompraid, "select id, 'O.C.-' + convert(varchar,id) + ' - ' + convert(varchar(10), fecha,103) as nombre from tblOrdenCompra where proveedorid='" & proveedorid.SelectedValue.ToString & "' and isnull(facturaid,0)=0 and isnull(gastoid,0)=0 order by id", 0)
            ObjData = Nothing
        End If
    End Sub

End Class