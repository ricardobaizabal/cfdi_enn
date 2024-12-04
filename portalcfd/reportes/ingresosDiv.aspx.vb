Imports System.Data
Imports System.Threading
Imports System.Globalization
Imports Telerik.Web.UI

Partial Class portalcfd_reportes_ingresosDiv
    Inherits System.Web.UI.Page
    Private ds As DataSet


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle
        '
        chkAll.Attributes.Add("onclick", "checkedAll(" & Me.Form.ClientID.ToString & ");")
        '
        lblReportsLegend.Text = Resources.Resource.lblReportsLegend & " - Reporte de facturación"
        If Not IsPostBack Then
            fechaini.SelectedDate = DateAdd(DateInterval.Day, -7, Now)
            fechafin.SelectedDate = Now
            Dim Objdata As New DataControl
            Objdata.Catalogo(clienteid, "exec pCatalogos @cmd=2", 0)
            Objdata.Catalogo(tipoid, "select id, nombre from tblTipoDocumento where id<>2", 0)
            Objdata.Catalogo(estatus_cobranzaid, "exec pMisInformes @cmd=11", 0)
            Objdata.Catalogo(tipo_pagoid, "exec pMisInformes @cmd=12", 0)
            Objdata = Nothing
            Call MuestraReporte()
        End If
    End Sub
    Private Sub MuestraReporte()
        '
        reporteGrid.Rebind()
        reporteGrid.CurrentPageIndex = 0
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pMisInformes @cmd=18, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @clienteid='" & clienteid.SelectedValue.ToString & "', @tipoid='" & tipoid.SelectedValue.ToString & "'")
        reporteGrid.DataSource = ds.Tables(0).DefaultView
        reporteGrid.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub
    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
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
                    e.Item.Cells(e.Item.Cells.Count - 2).ForeColor = Drawing.Color.Blue
                Else
                    e.Item.Cells(e.Item.Cells.Count - 2).ForeColor = Drawing.Color.Green
                End If
                '
                If (e.Item.DataItem("estatus") = 3) Or (e.Item.DataItem("escancelada")) Then
                    e.Item.Cells(e.Item.Cells.Count - 2).Text = "Cancelada"
                    e.Item.Cells(e.Item.Cells.Count - 2).ForeColor = Drawing.Color.Red
                    e.Item.Cells(e.Item.Cells.Count - 2).Font.Bold = True
                    Dim lnkFolio As LinkButton = CType(e.Item.FindControl("lnkFolio"), LinkButton)
                    lnkFolio.Visible = False
                    Dim lblFolio As Label = CType(e.Item.FindControl("lblFolio"), Label)
                    lblFolio.Text = e.Item.DataItem("folio").ToString
                    lblFolio.Visible = True
                End If
                '
                e.Item.Cells(e.Item.Cells.Count - 2).Font.Bold = True
            Case Telerik.Web.UI.GridItemType.Footer
                If ds.Tables(0).Rows.Count > 0 Then
                    '
                    If Not IsDBNull(ds.Tables(0).Compute("sum(importe)", "estatus<>3")) Then
                        e.Item.Cells(8).Text = FormatCurrency(ds.Tables(0).Compute("sum(importe)", "estatus<>3"), 2).ToString
                        e.Item.Cells(8).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(8).Font.Bold = True
                    End If
                    '
                    If Not IsDBNull(ds.Tables(0).Compute("sum(iva)", "estatus<>3")) Then
                        e.Item.Cells(9).Text = FormatCurrency(ds.Tables(0).Compute("sum(iva)", "estatus<>3"), 2).ToString
                        e.Item.Cells(9).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(9).Font.Bold = True
                    End If
                    '
                    If Not IsDBNull(ds.Tables(0).Compute("sum(retisr)", "estatus<>3")) Then
                        e.Item.Cells(10).Text = FormatCurrency(ds.Tables(0).Compute("sum(retisr)", "estatus<>3"), 2).ToString
                        e.Item.Cells(10).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(10).Font.Bold = True
                    End If
                    '
                    If Not IsDBNull(ds.Tables(0).Compute("sum(retiva)", "estatus<>3")) Then
                        e.Item.Cells(11).Text = FormatCurrency(ds.Tables(0).Compute("sum(retiva)", "estatus<>3"), 2).ToString
                        e.Item.Cells(11).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(11).Font.Bold = True
                    End If
                    '
                    If Not IsDBNull(ds.Tables(0).Compute("sum(retcuatro)", "estatus<>3")) Then
                        e.Item.Cells(12).Text = FormatCurrency(ds.Tables(0).Compute("sum(retcuatro)", "estatus<>3"), 2).ToString
                        e.Item.Cells(12).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(12).Font.Bold = True
                    End If
                    '
                    If Not IsDBNull(ds.Tables(0).Compute("sum(total)", "estatus<>3")) Then
                        e.Item.Cells(13).Text = FormatCurrency(ds.Tables(0).Compute("sum(total)", "estatus<>3"), 2).ToString
                        e.Item.Cells(13).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(13).Font.Bold = True
                    End If
                End If
        End Select

        If TypeOf e.Item Is GridCommandItem Then

            Dim cmditm As GridCommandItem = DirectCast(e.Item, GridCommandItem)
            Dim btn1 As Button = DirectCast(cmditm.FindControl("AddNewRecordButton"), Button)
            btn1.Visible = False
            Dim lnkbtn1 As LinkButton = DirectCast(cmditm.FindControl("InitInsertButton"), LinkButton)
            lnkbtn1.Visible = False
        End If

    End Sub

    Protected Sub reporteGrid_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles reporteGrid.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pMisInformes @cmd=18, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @clienteid='" & clienteid.SelectedValue.ToString & "', @tipoid='" & tipoid.SelectedValue.ToString & "'")
        reporteGrid.DataSource = ds.Tables(0)
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Protected Sub tipoid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tipoid.SelectedIndexChanged
        Call MuestraReporte()
    End Sub

    Protected Sub btnPayAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPayAll.Click
        Dim elementos As Integer = 0
        For Each row As GridDataItem In reporteGrid.Items
            Dim chkItem As CheckBox = CType(row.FindControl("chkcfdid"), CheckBox)
            If chkItem.Checked = True Then
                '
                elementos += 1
                '
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
                Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
                '
                Dim ObjData As New DataControl
                If Not fechapago.SelectedDate Is Nothing Then
                    ObjData.RunSQLQuery("exec pMisInformes @cmd=14, @estatus_cobranzaId='" & estatus_cobranzaid.SelectedValue.ToString & "', @tipo_pagoId='" & tipo_pagoid.SelectedValue.ToString & "', @referencia='" & referencia.Text & "', @cfdid='" & row.GetDataKeyValue("id").ToString & "', @fecha_pago='" & fechapago.SelectedDate.Value.ToShortDateString & "'")
                Else
                    ObjData.RunSQLQuery("exec pMisInformes @cmd=14, @estatus_cobranzaId='" & estatus_cobranzaid.SelectedValue.ToString & "', @tipo_pagoId='" & tipo_pagoid.SelectedValue.ToString & "', @referencia='" & referencia.Text & "', @cfdid='" & row.GetDataKeyValue("id").ToString & "'")
                End If
                ObjData = Nothing
                '
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
                Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
                '
                If System.Configuration.ConfigurationManager.AppSettings("usuarios") = 1 Then
                    Call ActualizaCFDUsuario(row.GetDataKeyValue("id"))
                End If
                '
            End If
        Next
        '
        If elementos > 0 Then
            Call ActualizaReporte()
            Call LimpiaControles()
            lblMensajeActualiza.Text = "Los documentos seleccionados han sido actualizados"
        Else
            lblMensajeActualiza.Text = "No se ha seleccionado ningún documento para actualización"
        End If
        '
    End Sub

    Private Sub ActualizaCFDUsuario(ByVal cfdid As Long)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pUsuarios @cmd=8, @userid='" & Session("userid").ToString & "', @cfdid='" & cfdid.ToString & "'")
        ObjData = Nothing
    End Sub

    Protected Sub btnGenerate_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Call ActualizaReporte()
    End Sub

    Private Sub ActualizaReporte()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pMisInformes @cmd=18, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @clienteid='" & clienteid.SelectedValue.ToString & "', @tipoid='" & tipoid.SelectedValue.ToString & "'")
        reporteGrid.DataSource = ds.Tables(0)
        reporteGrid.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub LimpiaControles()
        estatus_cobranzaid.SelectedIndex = 0
        tipo_pagoid.SelectedIndex = 0
        referencia.Text = ""
        fechapago.SelectedDate = Nothing
    End Sub
End Class
