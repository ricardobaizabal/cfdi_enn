Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Threading
Imports System.Globalization
Imports Telerik.Web.UI

Partial Public Class cuentas_por_pagar
    Inherits System.Web.UI.Page
    Private ds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            RequiredFieldValidator23.Enabled = False
            RequiredFieldValidator25.Enabled = False

            ddlBanco.Enabled = False
            RequiredFieldValidator1.Enabled = False
            RequiredFieldValidator2.Enabled = False
            ddlCuenta.Enabled = False
            txtNoChequex.Enabled = False

            Dim ObjData As New DataControl
            ObjData.Catalogo(proveedorid, "select id, razonsocial as nombre from tblMisProveedores order by nombre", 0, True)
            ObjData.Catalogo(estatusid, "select id, nombre as nombre from tblEstatusFactura order by nombre", 0)
            ObjData.Catalogo(metodopagoid, "select id, nombre from tblMetodoPago order by nombre", 0)
            ObjData.Catalogo(bancoid, "select id, nombre from tblBanco order by nombre", 0)
            ObjData.Catalogo(cuentaid, "select id, cuenta from tblCuentasBanco where bancoid='" & bancoid.SelectedValue.ToString & "' order by id", 0)

            ObjData.Catalogo(ddlEstatus, "select id, nombre as nombre from tblEstatusFactura order by nombre", 0)
            ObjData.Catalogo(ddlMetodoPago, "select id, nombre from tblMetodoPago order by nombre", 0)
            ObjData.Catalogo(ddlBanco, "select id, nombre from tblBanco order by nombre", 0)
            ObjData.Catalogo(ddlCuenta, "select id, cuenta from tblCuentasBanco where bancoid='" & ddlBanco.SelectedValue.ToString & "' order by id", 0)
            ObjData = Nothing

            'estatusid.Enabled = False
            'metodopagoid.Enabled = False
            'fechapago.Enabled = False
            bancoid.Enabled = False
            cuentaid.Enabled = False
            txtNoCheque.Enabled = False
            'btnPayAll.Enabled = False

            fechaini.SelectedDate = Date.Now.AddDays(-7)
            fechafin.SelectedDate = Date.Now

        End If
    End Sub

    Private Sub MuestraLista()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pCuentasPorPagar @cmd=1, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @proveedorid='" & proveedorid.SelectedValue.ToString & "'")
        facturaslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        facturaslist.DataSource = ds
        facturaslist.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub MuestraDetalle(ByVal documentoid As Long, ByVal tipodocumento As String)
        If tipodocumento = "Factura" Then
            Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

            Try

                Dim cmd As New SqlCommand("EXEC pCuentasPorPagar @cmd=4, @id='" & documentoid.ToString & "'", conn)

                conn.Open()

                Dim rs As SqlDataReader
                rs = cmd.ExecuteReader()

                If rs.Read Then

                    ddlEstatus.SelectedValue = rs("estatusid")
                    txtMonto.Text = rs("monto")
                    lblMoneda.Text = rs("moneda")
                    txtTipoCambio.Text = rs("tipo_cambio")

                    If rs("fecha_pago").ToString.Length > 0 Then
                        calFechaPago.SelectedDate = CDate(rs("fecha_pago"))
                    End If

                    ddlMetodoPago.SelectedValue = rs("metodo_pagoid")

                    If ddlMetodoPago.SelectedValue = 1 Then
                        ddlBanco.Enabled = False
                        ddlCuenta.Enabled = False
                        txtNoChequex.Enabled = False
                        RequiredFieldValidator1.Enabled = False
                        RequiredFieldValidator2.Enabled = False
                    ElseIf ddlMetodoPago.SelectedValue = 2 Or ddlMetodoPago.SelectedValue = 4 Then
                        ddlBanco.Enabled = True
                        ddlCuenta.Enabled = True
                        txtNoChequex.Enabled = False
                        RequiredFieldValidator1.Enabled = True
                        RequiredFieldValidator2.Enabled = False
                        If ddlMetodoPago.SelectedValue = 2 Then
                            txtNoChequex.Focus()
                            txtNoChequex.Enabled = True
                            ddlCuenta.Enabled = False
                            RequiredFieldValidator2.Enabled = True
                        End If
                    ElseIf ddlMetodoPago.SelectedValue = 3 Then
                        ddlBanco.Enabled = True
                        ddlCuenta.Enabled = True
                        txtNoChequex.Enabled = False
                        RequiredFieldValidator1.Enabled = True
                        RequiredFieldValidator2.Enabled = False
                    End If

                    Dim ObjData As New DataControl
                    ObjData.Catalogo(ddlBanco, "select id, nombre from tblBanco order by nombre", rs("bancoid"))
                    ObjData.Catalogo(ddlCuenta, "select id, cuenta from tblCuentasBanco where bancoid='" & rs("bancoid").ToString & "' order by id", rs("cuentaid"))
                    ObjData = Nothing

                    If ddlBanco.SelectedValue <> 0 Then
                        ddlCuenta.Enabled = True
                    Else
                        ddlCuenta.Enabled = False
                    End If

                    txtComentario.Text = rs("comentario")

                End If

            Catch ex As Exception

            Finally

                conn.Close()
                conn.Dispose()

            End Try
        ElseIf tipodocumento = "Gasto" Then

            'txtTipoCambio.Enabled = False

            Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

            Try

                Dim cmd As New SqlCommand("EXEC pCuentasPorPagar @cmd=5, @id='" & documentoid.ToString & "'", conn)

                conn.Open()

                Dim rs As SqlDataReader
                rs = cmd.ExecuteReader()

                If rs.Read Then

                    ddlEstatus.SelectedValue = rs("estatusid")
                    txtMonto.Text = rs("monto")
                    lblMoneda.Text = rs("moneda")
                    If rs("fecha_pago").ToString.Length > 0 Then
                        calFechaPago.SelectedDate = CDate(rs("fecha_pago"))
                    End If

                    ddlMetodoPago.SelectedValue = rs("metodo_pagoid")

                    If ddlMetodoPago.SelectedValue = 0 Or ddlMetodoPago.SelectedValue = 1 Then
                        ddlBanco.Enabled = False
                        ddlCuenta.Enabled = False
                        txtNoChequex.Enabled = False
                        RequiredFieldValidator1.Enabled = False
                        RequiredFieldValidator2.Enabled = False
                    ElseIf ddlMetodoPago.SelectedValue = 2 Or ddlMetodoPago.SelectedValue = 4 Then
                        ddlBanco.Enabled = True
                        ddlCuenta.Enabled = True
                        txtNoChequex.Enabled = False
                        RequiredFieldValidator1.Enabled = True
                        RequiredFieldValidator2.Enabled = False
                        If ddlMetodoPago.SelectedValue = 2 Then
                            txtNoChequex.Focus()
                            txtNoChequex.Enabled = True
                            ddlCuenta.Enabled = False
                            RequiredFieldValidator2.Enabled = True
                        End If
                    ElseIf ddlMetodoPago.SelectedValue = 3 Then
                        ddlBanco.Enabled = True
                        ddlCuenta.Enabled = True
                        txtNoChequex.Enabled = False
                        RequiredFieldValidator1.Enabled = True
                        RequiredFieldValidator2.Enabled = False
                    End If

                    Dim ObjData As New DataControl
                    ObjData.Catalogo(ddlBanco, "select id, nombre from tblBanco order by nombre", rs("bancoid"))
                    ObjData.Catalogo(ddlCuenta, "select id, cuenta from tblCuentasBanco where bancoid='" & rs("bancoid").ToString & "' order by id", rs("cuentaid"))
                    ObjData = Nothing

                    If ddlBanco.SelectedValue <> 0 Then
                        If ddlMetodoPago.SelectedValue <> 2 Then
                            ddlCuenta.Enabled = True
                        End If
                    Else
                        ddlCuenta.Enabled = False
                    End If

                    txtComentario.Text = rs("comentario")

                End If

            Catch ex As Exception


            Finally

                conn.Close()
                conn.Dispose()

            End Try
        End If
    End Sub

    Protected Sub facturaslist_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles facturaslist.ItemCommand
        Select Case e.CommandName

            Case "cmdEditar"
                Dim arg As String() = New String(1) {}
                arg = e.CommandArgument.ToString().Split(";"c)
                DocumentoID.Value = arg(0)
                TipoDocumento.Value = arg(1)

                Dim ObjData As New DataControl
                ObjData.Catalogo(ddlBanco, "select id, nombre from tblBanco order by nombre", 0)
                ObjData.Catalogo(ddlMetodoPago, "select id, nombre from tblMetodoPago order by nombre", 0)
                ObjData = Nothing

                txtNoChequex.Text = ""
                lblMensaje.Text = ""
                txtComentario.Text = ""

                MuestraDetalle(arg(0), arg(1))
                EditarDocumentoWindow.VisibleOnPageLoad = True

        End Select
    End Sub

    Protected Sub facturaslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles facturaslist.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "CuentasPorPagar" Then

                'Dim lnkEditar As LinkButton = DirectCast(dataItem.FindControl("lnkEditar"), LinkButton)
                'lnkEditar.Attributes.Add("onclick", "javascript: openRadWindow('" & e.Item.DataItem("id").ToString & "' , '" & e.Item.DataItem("tipo").ToString & "' , '" & proveedorid.SelectedValue.ToString & "' , '" & anioid.SelectedValue.ToString & "' , '" & mesid.SelectedValue.ToString & "'); return false;")

                Dim chkid As CheckBox = DirectCast(dataItem.FindControl("chkid"), CheckBox)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Convert.ToDecimal(e.Item.DataItem("total_pesos")) <= 0 Then
                        chkid.Enabled = False
                    End If
                End If

                'If proveedorid.SelectedValue = 0 Then
                '    chkid.Enabled = False
                'End If

            End If

        End If

        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Footer
                If ds.Tables(0).Rows.Count > 0 Then
                    '
                    If Not IsDBNull(ds.Tables(0).Compute("sum(total_dolares)", "estatus<>'Cancelada'")) Then
                        e.Item.Cells(9).Text = FormatCurrency(ds.Tables(0).Compute("sum(total_dolares)", "estatus<>'Cancelada'"), 2).ToString
                        e.Item.Cells(9).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(9).Font.Bold = True
                    End If
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
        ds = ObjData.FillDataSet("exec pCuentasPorPagar @cmd=1, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @proveedorid='" & proveedorid.SelectedValue.ToString & "'")
        facturaslist.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        facturaslist.DataSource = ds
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Protected Sub metodopagoid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles metodopagoid.SelectedIndexChanged
        EditarDocumentoWindow.VisibleOnPageLoad = False
        If metodopagoid.SelectedValue = 0 Then
            bancoid.Enabled = False
            bancoid.SelectedValue = 0
            cuentaid.Enabled = False
            cuentaid.SelectedValue = 0
            txtNoCheque.Enabled = False
            bancoid.SelectedValue = 0
            cuentaid.SelectedValue = 0
            txtNoCheque.Text = ""
            RequiredFieldValidator23.Enabled = False
            RequiredFieldValidator25.Enabled = False
        ElseIf metodopagoid.SelectedValue = 1 Then
            bancoid.Enabled = False
            cuentaid.Enabled = False
            txtNoCheque.Enabled = False
            bancoid.SelectedValue = 0
            cuentaid.SelectedValue = 0
            txtNoCheque.Text = ""
            RequiredFieldValidator23.Enabled = False
            RequiredFieldValidator25.Enabled = False
        ElseIf metodopagoid.SelectedValue = 2 Or metodopagoid.SelectedValue = 4 Then
            bancoid.Enabled = True
            cuentaid.Enabled = True
            txtNoCheque.Enabled = False
            RequiredFieldValidator23.Enabled = True
            RequiredFieldValidator25.Enabled = False
            If metodopagoid.SelectedValue = 2 Then
                txtNoCheque.Focus()
                'cuentaid.Enabled = False
                txtNoCheque.Enabled = True
                RequiredFieldValidator25.Enabled = True
            End If
        ElseIf metodopagoid.SelectedValue = 3 Then
            bancoid.Enabled = True
            cuentaid.Enabled = True
            txtNoCheque.Enabled = False
            RequiredFieldValidator23.Enabled = True
            RequiredFieldValidator25.Enabled = False
        End If
    End Sub

    Protected Sub proveedorid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles proveedorid.SelectedIndexChanged
        EditarDocumentoWindow.VisibleOnPageLoad = False
        Call MuestraLista()
        If proveedorid.SelectedValue = 0 Then
            btnPayAll.Enabled = False
            estatusid.SelectedValue = 0
            estatusid.Enabled = False
            metodopagoid.SelectedValue = 0
            metodopagoid.Enabled = False
            fechapago.Clear()
            fechapago.Enabled = False
            bancoid.SelectedValue = 0
            bancoid.Enabled = False
            cuentaid.SelectedValue = 0
            cuentaid.Enabled = False
            txtNoCheque.Text = ""
            txtNoCheque.Enabled = False
        Else
            btnPayAll.Enabled = True
            estatusid.SelectedValue = 0
            estatusid.Enabled = True
            metodopagoid.SelectedValue = 0
            metodopagoid.Enabled = True
            fechapago.Clear()
            fechapago.Enabled = True
            bancoid.SelectedValue = 0
            cuentaid.SelectedValue = 0
            txtNoCheque.Text = ""
        End If

        'For Each dataItem As Telerik.Web.UI.GridDataItem In facturaslist.MasterTableView.Items

        '    Dim chkid As CheckBox = DirectCast(dataItem.FindControl("chkid"), CheckBox)
        '    Dim total As Decimal = dataItem.GetDataKeyValue("total_pesos")
        '    If proveedorid.SelectedValue = 0 Then
        '        chkid.Enabled = False
        '    Else
        '        chkid.Enabled = True
        '    End If
        '    If Convert.ToDecimal(total) <= 0 Then
        '        chkid.Enabled = False
        '    End If
        'Next

    End Sub

    'Protected Sub anioid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles anioid.SelectedIndexChanged
    '    EditarDocumentoWindow.VisibleOnPageLoad = False
    '    Call MuestraLista()
    'End Sub

    'Protected Sub mesid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mesid.SelectedIndexChanged
    '    EditarDocumentoWindow.VisibleOnPageLoad = False
    '    Call MuestraLista()
    'End Sub

    Protected Sub btnPayAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPayAll.Click
        If Page.IsValid Then
            '
            Dim elementos As Integer = 0
            '
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
            '
            For Each dataItem As Telerik.Web.UI.GridDataItem In facturaslist.MasterTableView.Items

                Dim documentoId As Long = dataItem.GetDataKeyValue("id").ToString
                Dim TipoDocumento As String = dataItem.GetDataKeyValue("tipo").ToString
                Dim chkid As CheckBox = DirectCast(dataItem.FindControl("chkid"), CheckBox)

                Dim ObjData As New DataControl
                If chkid.Checked Then
                    elementos += 1
                    If TipoDocumento = "Factura" Then
                        ObjData.FillDataSet("exec pCuentasPorPagar @cmd=7, @estatusid='" & estatusid.SelectedValue.ToString & "', @metodo_pagoid='" & metodopagoid.SelectedValue.ToString & "', @fecha_pago='" & fechapago.SelectedDate.Value.ToShortDateString & "', @bancoid='" & bancoid.SelectedValue.ToString & "', @cuentaid='" & cuentaid.SelectedValue.ToString & "', @no_cheque='" & txtNoCheque.Text.ToString & "', @id='" & documentoId.ToString & "', @userid='" & Session("userid").ToString & "'")
                    ElseIf TipoDocumento = "Gasto" Then
                        ObjData.FillDataSet("exec pCuentasPorPagar @cmd=8, @estatusid='" & estatusid.SelectedValue.ToString & "', @metodo_pagoid='" & metodopagoid.SelectedValue.ToString & "', @fecha_pago='" & fechapago.SelectedDate.Value.ToShortDateString & "', @bancoid='" & bancoid.SelectedValue.ToString & "', @cuentaid='" & cuentaid.SelectedValue.ToString & "', @no_cheque='" & txtNoCheque.Text.ToString & "', @id='" & documentoId.ToString & "', @userid='" & Session("userid").ToString & "'")
                    End If
                End If
                ObjData = Nothing
            Next
            '
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
            '
            If elementos > 0 Then
                Call MuestraLista()
                Call LimpiaControles()
                lblMensajeActualiza.ForeColor = Drawing.Color.Green
                lblMensajeActualiza.Text = "Los documentos seleccionados han sido actualizados"
            Else
                lblMensajeActualiza.ForeColor = Drawing.Color.Red
                lblMensajeActualiza.Text = "No se ha seleccionado ningún documento para actualización"
            End If
            '
        End If
    End Sub

    Private Sub LimpiaControles()
        estatusid.SelectedValue = 0
        metodopagoid.SelectedValue = 0
        fechapago.Clear()
        bancoid.SelectedValue = 0
        cuentaid.SelectedValue = 0
        txtNoCheque.Text = ""
    End Sub

    Protected Sub btnConsulta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsulta.Click
        Call MuestraLista()
        EditarDocumentoWindow.VisibleOnPageLoad = False
    End Sub

    Protected Sub ddlMetodoPago_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMetodoPago.SelectedIndexChanged
        If ddlMetodoPago.SelectedValue = 0 Or ddlMetodoPago.SelectedValue = 1 Then
            ddlBanco.Enabled = False
            ddlBanco.SelectedValue = 0
            ddlCuenta.Enabled = False
            ddlCuenta.SelectedValue = 0
            txtNoChequex.Enabled = False
            txtNoChequex.Text = ""
            RequiredFieldValidator1.Enabled = False
            RequiredFieldValidator2.Enabled = False
        ElseIf ddlMetodoPago.SelectedValue = 2 Or ddlMetodoPago.SelectedValue = 4 Then
            ddlBanco.Enabled = True
            ddlCuenta.Enabled = True
            txtNoChequex.Enabled = False
            RequiredFieldValidator1.Enabled = True
            RequiredFieldValidator2.Enabled = False
            If ddlMetodoPago.SelectedValue = 2 Then
                txtNoChequex.Focus()
                txtNoChequex.Enabled = True
                'ddlCuenta.Enabled = False
                RequiredFieldValidator2.Enabled = True
            End If
        ElseIf ddlMetodoPago.SelectedValue = 3 Then
            ddlBanco.Enabled = True
            ddlCuenta.Enabled = True
            txtNoChequex.Enabled = False
            RequiredFieldValidator1.Enabled = True
            RequiredFieldValidator2.Enabled = False
        End If
    End Sub

    Protected Sub ddlBanco_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBanco.SelectedIndexChanged
        Dim ObjData As New DataControl
        ObjData.Catalogo(ddlCuenta, "select id, cuenta from tblCuentasBanco where bancoid='" & ddlBanco.SelectedValue.ToString & "'", 0)
        ObjData = Nothing
        If ddlBanco.SelectedValue <> 0 Then
            If ddlMetodoPago.SelectedValue <> 2 Then
                ddlCuenta.Enabled = True
            End If
        Else
            ddlCuenta.Enabled = False
        End If
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            '
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
            '
            If TipoDocumento.Value = "Factura" Then
                Dim ObjData As New DataControl
                ObjData.FillDataSet("exec pCuentasPorPagar @cmd=2, @fecha_pago='" & calFechaPago.SelectedDate.Value.ToShortDateString & "', @estatusid='" & ddlEstatus.SelectedValue.ToString & "', @metodo_pagoid='" & ddlMetodoPago.SelectedValue.ToString & "', @bancoid='" & ddlBanco.SelectedValue.ToString & "', @cuentaid='" & ddlCuenta.SelectedValue.ToString & "', @comentario='" & txtComentario.Text & "', @total='" & txtMonto.Text.ToString & "', @tipo_cambio='" & txtTipoCambio.Text.ToString & "', @no_cheque='" & txtNoChequex.Text.ToString & "', @id='" & DocumentoID.Value.ToString & "', @userid='" & Session("userid").ToString & "'")
                ObjData = Nothing
            ElseIf TipoDocumento.Value = "Gasto" Then
                Dim ObjData As New DataControl
                ObjData.FillDataSet("exec pCuentasPorPagar @cmd=3, @fecha_pago='" & calFechaPago.SelectedDate.Value.ToShortDateString & "', @estatusid='" & ddlEstatus.SelectedValue.ToString & "', @metodo_pagoid='" & ddlMetodoPago.SelectedValue.ToString & "', @bancoid='" & ddlBanco.SelectedValue.ToString & "', @cuentaid='" & ddlCuenta.SelectedValue.ToString & "', @comentario='" & txtComentario.Text & "', @monto='" & txtMonto.Text.ToString & "', @no_cheque='" & txtNoChequex.Text.ToString & "', @id='" & DocumentoID.Value.ToString & "', @userid='" & Session("userid").ToString & "'")
                ObjData = Nothing
            End If
            '
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
            '
            Call MuestraLista()
            '
            lblMensaje.ForeColor = Drawing.Color.Green
            lblMensaje.Text = "Se actualizó con éxito la información "
            EditarDocumentoWindow.VisibleOnPageLoad = False
        Catch ex As Exception
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Text = "Error inesperado --> " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub bancoid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles bancoid.SelectedIndexChanged
        Dim ObjData As New DataControl
        ObjData.Catalogo(cuentaid, "select id, cuenta from tblCuentasBanco where bancoid='" & bancoid.SelectedValue.ToString & "'", 0)
        ObjData = Nothing
        If bancoid.SelectedValue <> 0 Then
            If metodopagoid.SelectedValue <> 2 Then
                cuentaid.Enabled = True
            End If
        Else
            cuentaid.Enabled = False
        End If
    End Sub

End Class