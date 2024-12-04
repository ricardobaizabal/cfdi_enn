Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class portalcfd_Clientes
    Inherits System.Web.UI.Page

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then

            Dim DataControl As New DataControl
            txtZipCod.DataSource = ObtenerCodigoPostal()
            DataControl = Nothing

            '''''''''''''''''''
            'Fieldsets Legends'
            '''''''''''''''''''

            lblClientsListLegend.Text = Resources.Resource.lblClientsListLegend
            lblClientEditLegend.Text = Resources.Resource.lblClientEditLegend

            '''''''''''''''''''''''''''''''''
            'Combobox Values & Empty Message'
            '''''''''''''''''''''''''''''''''

            ''''''''''''''
            'Label Titles'
            ''''''''''''''

            lblSocialReason.Text = Resources.Resource.lblSocialReason
            lblContact.Text = Resources.Resource.lblContact
            lblContactEmail.Text = Resources.Resource.lblContactEmail
            lblContactPhone.Text = Resources.Resource.lblContactPhone
            lblStreet.Text = Resources.Resource.lblStreet
            lblExtNumber.Text = Resources.Resource.lblExtNumber
            lblIntNumber.Text = Resources.Resource.lblIntNumber
            lblColony.Text = Resources.Resource.lblColony
            lblCountry.Text = Resources.Resource.lblCountry
            lblState.Text = Resources.Resource.lblState
            lblTownship.Text = Resources.Resource.lblTownship
            lblZipCode.Text = Resources.Resource.lblZipCode
            lblRFC.Text = Resources.Resource.lblRFC
            'lblMetodoPago.Text = Resources.Resource.lblMetodoPago
            lblNumCtaPago.Text = Resources.Resource.lblNumCtaPago

            '''''''''''''''''''
            'Validators Titles'
            '''''''''''''''''''

            RequiredFieldValidator1.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator2.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator3.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator4.Text = Resources.Resource.validatorMessage
            valZipCode1.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator6.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator7.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator8.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator9.Text = Resources.Resource.validatorMessage
            valTipoContribuyente.Text = Resources.Resource.validatorMessage

            RegularExpressionValidator1.Text = Resources.Resource.validatorMessageEmail
            valRFC.Text = Resources.Resource.validatorMessageRfc

            ''''''''''''''''
            'Buttons Titles'
            ''''''''''''''''

            btnAddClient.Text = Resources.Resource.btnAddClient
            btnSaveClient.Text = Resources.Resource.btnSave
            btnCancel.Text = Resources.Resource.btnCancel

            Dim ObjData As New DataControl
            ObjData.Catalogo(condicionesid, "select id, nombre from tblCondiciones", 0)
            ObjData.Catalogo(tipoContribuyenteid, "select id, nombre from tblTipoContribuyente", 0)
            ObjData.Catalogo(formapagoid, "select id, id + ' - ' + nombre as nombre from tblFormaPago order by nombre", 0)
            ObjData.Catalogo(paisid, "EXEC pCatalogoPais @cmd=6", 0)
            ObjData.Catalogo(estadoid, "select id, nombre from tblEstado order by nombre", 0)
            ObjData.Catalogo(cmbTipoPrecio, "select id, nombre from tblTipoPrecio order by nombre", 0)
            ObjData = Nothing

        End If

    End Sub

    Function ObtenerCodigoPostal() As DataSet
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pCatalogos @cmd=6, @estadoid='" & estadoid.SelectedValue & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return ds

    End Function

#End Region

#Region "Load List Of Clients"

    Function GetClients() As DataSet

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pMisClientes  @cmd=1, @clienteUnionId='" & Session("clienteid") & "', @txtSearch='" & txtSearch.Text & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return ds

    End Function

#End Region

#Region "Telerik Grid Clients Loading Events"

    Protected Sub clientslist_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles clientslist.NeedDataSource

        clientslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
        clientslist.DataSource = GetClients()

    End Sub

#End Region

#Region "Telerik Grid Language Modification(Spanish)"

    Protected Sub clientslist_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles clientslist.Init

        clientslist.PagerStyle.NextPagesToolTip = "Ver mas"
        clientslist.PagerStyle.NextPageToolTip = "Siguiente"
        clientslist.PagerStyle.PrevPagesToolTip = "Ver más"
        clientslist.PagerStyle.PrevPageToolTip = "Atrás"
        clientslist.PagerStyle.LastPageToolTip = "Última Página"
        clientslist.PagerStyle.FirstPageToolTip = "Primera Página"
        clientslist.PagerStyle.PagerTextFormat = "{4}    Página {0} de {1}, Registros {2} al {3} de {5}"
        clientslist.SortingSettings.SortToolTip = "Ordernar"
        clientslist.SortingSettings.SortedAscToolTip = "Ordenar Asc"
        clientslist.SortingSettings.SortedDescToolTip = "Ordenar Desc"


        Dim menu As Telerik.Web.UI.GridFilterMenu = clientslist.FilterMenu
        Dim i As Integer = 0

        While i < menu.Items.Count

            If menu.Items(i).Text = "NoFilter" Or menu.Items(i).Text = "Contains" Then
                i = i + 1
            Else
                menu.Items.RemoveAt(i)
            End If

        End While

        Call ModificaIdiomaGrid()

    End Sub

    Private Sub ModificaIdiomaGrid()

        clientslist.GroupingSettings.CaseSensitive = False

        Dim Menu As Telerik.Web.UI.GridFilterMenu = clientslist.FilterMenu
        Dim item As Telerik.Web.UI.RadMenuItem

        For Each item In Menu.Items

            ''''''''''''''''''''''''''''''''''''''''''''''
            'Change The Text For The StartsWith Menu Item'
            ''''''''''''''''''''''''''''''''''''''''''''''

            If item.Text = "StartsWith" Then
                item.Text = "Empieza con"
            End If

            If item.Text = "NoFilter" Then
                item.Text = "Sin Filtro"
            End If

            If item.Text = "Contains" Then
                item.Text = "Contiene"
            End If

            If item.Text = "EndsWith" Then
                item.Text = "Termina con"
            End If

        Next

    End Sub

#End Region

#Region "Telerik Grid Clients Editing & Deleting Events"

    Protected Sub clientslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles clientslist.ItemDataBound

        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Clients" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('" & Resources.Resource.ClientsDeleteConfirmationMessage & "');")

            End If

        End If

    End Sub

    Protected Sub clientslist_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles clientslist.ItemCommand

        Select Case e.CommandName

            Case "cmdEdit"
                EditClient(e.CommandArgument)

            Case "cmdDelete"
                DeleteClient(e.CommandArgument)

        End Select

    End Sub

    Private Sub DeleteClient(ByVal id As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pMisClientes @cmd='3', @clienteId ='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelClientRegistration.Visible = False

            clientslist.DataSource = GetClients()
            clientslist.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub EditClient(ByVal id As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pMisClientes @cmd='2', @clienteId='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                txtSocialReason.Text = rs("razonsocial")
                txtDenominacionRaznScial.Text = rs("denominacion_razon_social")
                txtContact.Text = rs("contacto")
                txtContactEmail.Text = rs("email_contacto")
                txtContactPhone.Text = rs("telefono_contacto")
                txtStreet.Text = rs("fac_calle")
                txtExtNumber.Text = rs("fac_num_ext")
                txtIntNumber.Text = rs("fac_num_int")
                txtColony.Text = rs("fac_colonia")
                txtTownship.Text = rs("fac_municipio")
                txtZipCode.Text = rs("fac_cp")
                txtRFC.Text = rs("rfc")
                txtNumCtaPago.Text = rs("numctapago")

                Dim ObjData As New DataControl
                ObjData.Catalogo(condicionesid, "select id, nombre from tblCondiciones", rs("condicionesid"))
                ObjData.Catalogo(tipoContribuyenteid, "select id, nombre from tblTipoContribuyente", rs("tipoContribuyenteid"))
                Call SetCmbRegFiscal(rs("tipoContribuyenteid"), rs("regimenfiscalid"))
                ObjData.Catalogo(formapagoid, "select id, id + ' - ' + nombre as nombre from tblFormaPago order by nombre", rs("formapagoid"))
                ObjData.Catalogo(paisid, "EXEC pCatalogoPais @cmd=6", rs("fac_paisid"))
                ObjData.Catalogo(estadoid, "select id, nombre from tblEstado order by nombre", rs("fac_estadoid"))
                ObjData.Catalogo(cmbTipoPrecio, "select id, nombre from tblTipoPrecio order by nombre", rs("tipoprecioid"))
                ObjData = Nothing

                If paisid.SelectedValue <> 1 Then
                    txtStates.Text = rs("fac_estado")
                    estadoid.SelectedValue = 0
                    ValidarPais()
                Else
                    txtZipCod.Entries.Add(New AutoCompleteBoxEntry(rs("fac_cp"), rs("fac_cp")))
                    'txtZipCod.SelectedValue = rs("fac_cp")

                    Dim DataControl As New DataControl
                    txtZipCod.DataSource = ObtenerCodigoPostal()
                    txtZipCod.DataBind()
                    DataControl = Nothing

                    ValidarPais()
                End If

                panelClientRegistration.Visible = True
                RadTabStrip1.Tabs(1).Enabled = True
                cuentasList.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
                cuentasList.DataSource = ObtenerCuentas()
                cuentasList.DataBind()

                InsertOrUpdate.Value = 1
                ClientsID.Value = id
                contactosadicionaleslist_NeedData()
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

#End Region

#Region "Telerik Grid Clients Column Names (From Resource File)"

    'Protected Sub clientslist_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles clientslist.ItemCreated

    '    If TypeOf e.Item Is GridHeaderItem Then

    '        Dim header As GridHeaderItem = CType(e.Item, GridHeaderItem)

    '        If e.Item.OwnerTableView.Name = "Clients" Then

    '            header("razonsocial").Text = Resources.Resource.gridColumnNameSocialReason
    '            header("contacto").Text = Resources.Resource.gridColumnNameContact
    '            header("telefono_contacto").Text = Resources.Resource.gridColumnNameContactPhone
    '            header("rfc").Text = Resources.Resource.gridColumnNameRFC

    '        End If

    '    End If

    'End Sub

#End Region

#Region "Display Client Data Panel"

    Protected Sub btnAddClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddClient.Click

        InsertOrUpdate.Value = 0

        txtSocialReason.Text = ""
        txtDenominacionRaznScial.Text = ""
        txtContact.Text = ""
        txtContactEmail.Text = ""
        txtContactPhone.Text = ""
        txtStreet.Text = ""
        txtExtNumber.Text = ""
        txtIntNumber.Text = ""
        txtColony.Text = ""
        txtTownship.Text = ""
        txtZipCode.Text = ""
        txtRFC.Text = ""

        condicionesid.SelectedValue = 0
        tipoContribuyenteid.SelectedValue = 0
        formapagoid.SelectedValue = 0
        estadoid.SelectedValue = 0
        regimenid.SelectedValue = 0

        panelClientRegistration.Visible = True
        RadTabStrip1.Tabs(1).Enabled = False

    End Sub

#End Region

#Region "Save Client"

    Protected Sub btnSaveClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveClient.Click

        Dim fac_cp As String = ""

        If paisid.SelectedValue <> 1 Then
            fac_cp = txtZipCode.Text.Trim.ToString
        Else
            fac_cp = Replace(txtZipCod.Text.Trim.ToString, ";", "")
        End If

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            If InsertOrUpdate.Value = 0 Then

                Dim cmd As New SqlCommand("EXEC pMisClientes @cmd=4, @clienteUnionId='" & Session("clienteid").ToString & "', @razonsocial='" & txtSocialReason.Text & "', @contacto='" & txtContact.Text & "', @email_contacto='" & txtContactEmail.Text & "', @telefono_contacto='" & txtContactPhone.Text & "', @fac_calle='" & txtStreet.Text & "', @fac_num_int='" & txtIntNumber.Text & "', @fac_num_ext='" & txtExtNumber.Text & "', @fac_colonia='" & txtColony.Text & "', @fac_municipio='" & txtTownship.Text & "', @fac_paisid='" & paisid.SelectedValue & "', @fac_estadoid='" & estadoid.SelectedValue.ToString & "', @fac_estado='" & txtStates.Text & "', @fac_cp='" & fac_cp.ToString & "', @fac_rfc='" & txtRFC.Text & "', @condicionesid='" & condicionesid.SelectedValue.ToString & "', @tipocontribuyenteid='" & tipoContribuyenteid.SelectedValue.ToString & "', @formapagoid='" & formapagoid.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @tipoprecioid='" & cmbTipoPrecio.SelectedValue.ToString & "', @regimenfiscalid='" & regimenid.SelectedValue & "', @denominacion_razon_social='" & txtDenominacionRaznScial.Text & "'", conn)


                conn.Open()

                cmd.ExecuteReader()

                panelClientRegistration.Visible = False

                clientslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                clientslist.DataSource = GetClients()
                clientslist.DataBind()

                conn.Close()
                conn.Dispose()

            Else

                Dim cmd As New SqlCommand("EXEC pMisClientes @cmd=5, @clienteid='" & ClientsID.Value & "', @razonsocial='" & txtSocialReason.Text & "', @contacto='" & txtContact.Text & "', @email_contacto='" & txtContactEmail.Text & "', @telefono_contacto='" & txtContactPhone.Text & "', @fac_calle='" & txtStreet.Text & "', @fac_num_int='" & txtIntNumber.Text & "', @fac_num_ext='" & txtExtNumber.Text & "', @fac_colonia='" & txtColony.Text & "', @fac_municipio='" & txtTownship.Text & "', @fac_paisid='" & paisid.SelectedValue & "', @fac_estadoid='" & estadoid.SelectedValue.ToString & "', @fac_estado='" & txtStates.Text & "', @fac_cp='" & fac_cp & "', @fac_rfc='" & txtRFC.Text & "', @condicionesid='" & condicionesid.SelectedValue.ToString & "', @tipocontribuyenteid='" & tipoContribuyenteid.SelectedValue.ToString & "', @formapagoid='" & formapagoid.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @tipoprecioid='" & cmbTipoPrecio.SelectedValue.ToString & "', @regimenfiscalid='" & regimenid.SelectedValue & "', @denominacion_razon_social='" & txtDenominacionRaznScial.Text & "'", conn)


                conn.Open()

                cmd.ExecuteReader()

                panelClientRegistration.Visible = False

                clientslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                clientslist.DataSource = GetClients()
                clientslist.DataBind()

                conn.Close()
                conn.Dispose()

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

#End Region

#Region "Cancel Client (Save/Edit)"

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        InsertOrUpdate.Value = 0

        txtSocialReason.Text = ""
        txtContact.Text = ""
        txtContactEmail.Text = ""
        txtContactPhone.Text = ""
        txtStreet.Text = ""
        txtExtNumber.Text = ""
        txtIntNumber.Text = ""
        txtColony.Text = ""
        txtTownship.Text = ""
        txtZipCode.Text = ""
        txtRFC.Text = ""

        condicionesid.SelectedValue = 0
        tipoContribuyenteid.SelectedValue = 0
        formapagoid.SelectedValue = 0
        estadoid.SelectedValue = 0

        panelClientRegistration.Visible = False

    End Sub

#End Region

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        clientslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
        clientslist.DataSource = GetClients()
        clientslist.DataBind()
    End Sub

    Protected Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        txtSearch.Text = ""
        clientslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
        clientslist.DataSource = GetClients()
        clientslist.DataBind()
    End Sub

    Protected Sub paisid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles paisid.SelectedIndexChanged
        ValidarPais()
    End Sub

    Public Sub ValidarPais()
        If paisid.SelectedValue <> 1 Then
            estadoid.SelectedValue = 0
            estadoid.Visible = False
            RequiredFieldValidator6.Enabled = False
            txtStates.Visible = True
            txtStates.Enabled = True
            RequiredFieldValidator10.Enabled = True

            txtZipCode.Visible = True
            valZipCode1.Enabled = True

            valZipCode2.Enabled = False
            txtZipCod.Visible = False
        Else
            'Mexico
            paisid.Visible = True
            estadoid.Visible = True
            RequiredFieldValidator6.Enabled = True

            txtStates.Visible = False
            txtStates.Enabled = False
            RequiredFieldValidator10.Enabled = False

            txtZipCode.Visible = False
            valZipCode1.Enabled = False

            valZipCode2.Enabled = True
            txtZipCod.Visible = True
        End If
    End Sub

    Private Sub txtZipCod_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtZipCod.Load
        Dim ObjData As New DataControl
        txtZipCod.DataSource = ObtenerCodigoPostal()
        txtZipCod.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub estadoid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles estadoid.SelectedIndexChanged
        Dim ObjData As New DataControl
        txtZipCod.DataSource = ObtenerCodigoPostal()
        txtZipCod.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If Page.IsValid Then
            Dim objData As New DataControl
            Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
            Dim sql As String = ""
            Dim IdCuentas As Integer = 0

            If CuentaID.Value = 0 Then
                IdCuentas = objData.RunSQLScalarQuery("EXEC pCatalogoCuentas @cmd=1, @banconacional='" & txtBanco.Text & "', @bancoextranjero='" & txtBancoExtr.Text & "',@rfc='" & txtRFCBAK.Text & "', @numctapago='" & txtCuenta.Text & "', @predeterminadoBit='" & chkPredeterminado.Checked & "', @clienteid='" & ClientsID.Value & "'")
                clearItems()
            Else
                objData.RunSQLScalarQuery("EXEC pCatalogoCuentas @cmd=4, @banconacional='" & txtBanco.Text & "', @bancoextranjero='" & txtBancoExtr.Text & "', @rfc='" & txtRFCBAK.Text & "', @numctapago='" & txtCuenta.Text & "',@predeterminadoBit='" & chkPredeterminado.Checked & "', @clienteid='" & ClientsID.Value & "', @id='" & CuentaID.Value & "'")
                clearItems()
                objData = Nothing
            End If

            cuentasList.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
            cuentasList.DataSource = ObtenerCuentas()
            cuentasList.DataBind()
        End If
    End Sub

    Function ObtenerCuentas() As DataSet

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim qry As String = "EXEC pCatalogoCuentas @cmd=5, @clienteid='" & ClientsID.Value & "'"

        Dim cmd As New SqlDataAdapter(qry, conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return ds

    End Function

    Private Sub DeleteCuenta(ByVal id As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoCuentas @cmd=3, @id='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            cuentasList.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
            cuentasList.DataSource = ObtenerCuentas()
            cuentasList.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub CargarCuenta()

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoCuentas @cmd=2, @id='" & CuentaID.Value & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                txtBanco.Text = rs("banconacional")
                txtBancoExtr.Text = rs("bancoextranjero")
                txtCuenta.Text = rs("numctapago")
                txtRFCBAK.Text = rs("rfc")
                chkPredeterminado.Checked = rs("predeterminadoBit")
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        clearItems()
    End Sub

    Private Sub cuentasList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles cuentasList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                CuentaID.Value = e.CommandArgument
                Call CargarCuenta()
                btnCancelar.Visible = True
            Case "cmdDelete"
                Call DeleteCuenta(e.CommandArgument)
        End Select
    End Sub

    Private Sub cuentasList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles cuentasList.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim imgPredeterminado As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgPredeterminado"), System.Web.UI.WebControls.Image)
                imgPredeterminado.Visible = e.Item.DataItem("predeterminadoBit")
        End Select
    End Sub

    Sub clearItems()
        txtBanco.Text = ""
        txtBancoExtr.Text = ""
        txtCuenta.Text = ""
        chkPredeterminado.Checked = False
        txtBanco.Focus()
        CuentaID.Value = 0
    End Sub

    Private Sub cuentasList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles cuentasList.NeedDataSource
        cuentasList.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
        cuentasList.DataSource = ObtenerCuentas()
    End Sub

#Region "Contactos adicionales"
    Protected Sub btnSaveContAdicional_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveContAdicional.Click
        ' cmd=2 
        If ContactoAdicionalID.Value <> 0 Then
            SaveContAdicional(4)
        Else
            SaveContAdicional()
        End If
    End Sub
    Private Sub SaveContAdicional(Optional ByVal cmd As Long = 1)
        Dim Obj As New DataControl
        Dim cmdSQL As String = "exec pMisClientesCorreos @cmd=" & cmd & ", @clienteid='" & ClientsID.Value & "',@nombre='" & txtNombreContAdicional.Text & "', @correo='" & txtCorreoContAdicional.Text & "', @id='" & ContactoAdicionalID.Value & "'"
        Obj.RunSQLQuery(cmdSQL)
        cleanItemsContAdicional()
        contactosadicionaleslist_NeedData()
    End Sub
    Private Sub CargaContactoAdiconal()
        Dim Obj As New DataControl
        Dim cmd As String = "exec pMisClientesCorreos @cmd=3, @id='" & ContactoAdicionalID.Value & "'"
        Dim result As New DataSet
        result = Obj.FillDataSet(cmd)
        For Each row As DataRow In result.Tables(0).Rows
            txtNombreContAdicional.Text = row("nombre")
            txtCorreoContAdicional.Text = row("correo")
            btnCancelContAdicional.Visible = True
        Next
    End Sub
    Private Sub cleanItemsContAdicional()
        txtNombreContAdicional.Text = ""
        txtCorreoContAdicional.Text = ""
        btnCancelContAdicional.Visible = False
    End Sub

    Private Sub contactosadicionaleslist_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles contactosadicionaleslist.ItemCommand

        ContactoAdicionalID.Value = e.CommandArgument

        Select Case e.CommandName
            Case "cmdEdit"
                Call CargaContactoAdiconal()
            Case "cmdDelete"
                Call SaveContAdicional(5)
        End Select
    End Sub

    Private Sub contactosadicionaleslist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles contactosadicionaleslist.NeedDataSource
        contactosadicionaleslist_NeedData("off")
    End Sub
    Private Sub contactosadicionaleslist_NeedData(Optional ByVal state As String = "on")
        Dim Obj As New DataControl
        Dim cmd As String = "exec pMisClientesCorreos @cmd=2, @clienteid=" & ClientsID.Value
        contactosadicionaleslist.DataSource = Obj.FillDataSet(cmd)
        If state = "on" Then
            contactosadicionaleslist.DataBind()
        End If
        If contactosadicionaleslist.Items.Count < 1 Then
            Dim dt As New DataTable
            contactosadicionaleslist.DataSource = dt
        End If
        
    End Sub

    Private Sub btnCancelContAdicional_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelContAdicional.Click
        cleanItemsContAdicional()
    End Sub

#End Region

    Private Sub SetCmbRegFiscal(Optional ByVal contribuyenteid As Integer = 0, Optional ByVal sel As Integer = 0)
        Dim ObjData As New DataControl
        Dim sqlwhere As String
        Select Case tipoContribuyenteid.SelectedValue
            Case 1
                sqlwhere = "where fisica='Sí' "
            Case 2
                sqlwhere = "where moral='Sí' "
            Case Else
                sqlwhere = ""
        End Select
        ObjData.Catalogo(regimenid, "select id, id + ' - ' + nombre as descripcion " & "from tblRegimenFiscal " & sqlwhere & " order by nombre ", sel)
        ObjData = Nothing
    End Sub

    Protected Sub tipoContribuyenteid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tipoContribuyenteid.SelectedIndexChanged
        SetCmbRegFiscal(tipoContribuyenteid.SelectedValue)
    End Sub

End Class