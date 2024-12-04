Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.IO

Partial Class portalcfd_Datos
    Inherits System.Web.UI.Page

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        LoadDatosCodigoPostal(txtZipCod)

        If Not IsPostBack Then

            '''''''''''''''''''
            'Fieldsets Legends'
            '''''''''''''''''''

            lblDataLegend.Text = Resources.Resource.lblDataLegend

            '''''''''''''''''''''''''''''''''
            'Combobox Values & Empty Message'
            '''''''''''''''''''''''''''''''''

            Dim TelerikRadComboBox As New FillRadComboBox
            TelerikRadComboBox.FillRadComboBox(cmbStates, "EXEC pCatalogos @cmd=1")

            cmbStates.Text = Resources.Resource.cmbEmptyMessage

            Dim ObjData As New DataControl
            ObjData.Catalogo(plantillaid, "select id, nombre from tblPlantilla order by nombre", 0)
            ObjData.Catalogo(regimenid, "select id, nombre from tblRegimenFiscal order by nombre", 0)
            ObjData = Nothing

            ''''''''''''''
            'Label Titles'
            ''''''''''''''

            lblSocialReason.Text = Resources.Resource.lblSocialReason
            lblEmailContact.Text = "Correo Electrónico"
            'lblPassword.Text = Resources.Resource.lblPassword
            lblStreet.Text = Resources.Resource.lblStreet
            lblExtNumber.Text = Resources.Resource.lblExtNumber
            lblIntNumber.Text = Resources.Resource.lblIntNumber
            lblColony.Text = Resources.Resource.lblColony
            lblCountry.Text = Resources.Resource.lblCountry
            lblState.Text = Resources.Resource.lblState
            lblTownship.Text = Resources.Resource.lblTownship
            lblZipCode.Text = Resources.Resource.lblZipCode
            lblRFC.Text = Resources.Resource.lblRFC
            lblLogo.Text = Resources.Resource.lblLogo
            lblRegimen.Text = Resources.Resource.lblRegimen

            '''''''''''''''''''
            'Validators Titles'
            '''''''''''''''''''
            RequiredFieldValidator1.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator2.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator3.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator4.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator5.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator6.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator7.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator8.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator9.Text = Resources.Resource.validatorMessage
            valRegimen.Text = Resources.Resource.validatorMessage
            ValidateExtensions.Text = Resources.Resource.InvalidExtension

            ''''''''''''''''
            'Buttons Titles'
            ''''''''''''''''

            btnSaveData.Text = Resources.Resource.btnSave

            '''''''''''''
            'Data Values'
            '''''''''''''

            DisplayData()

        End If
        If Session("admin") = 1 Then
            pnlConfiguracionCorreo.Visible = True
            RadUpload1.Enabled = True
            RadUpload2.Enabled = True
            plantillaid.Enabled = True
        Else
            RadUpload1.Enabled = False
            RadUpload2.Enabled = False
            plantillaid.Enabled = False
        End If
    End Sub

    Private Sub LoadDatosCodigoPostal(ByVal autoCompleteBox As RadAutoCompleteBox)
        Dim ObjData As New DataControl
        autoCompleteBox.DataSource = ObtenerCodigoPostal()
        ObjData = Nothing
    End Sub

    Function ObtenerCodigoPostal() As DataSet
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pCatalogos @cmd=6, @estadoid='" & cmbStates.SelectedValue & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return ds

    End Function

#End Region

#Region "Display Data"

    Private Sub DisplayData()

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pCliente @cmd='3', @clienteId='" & Session("clienteid") & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                txtSocialReason.Text = rs("razonsocial")
                txtEmailContact.Text = rs("email_contacto")
                txtStreet.Text = rs("fac_calle")
                txtExtNumber.Text = rs("fac_num_ext")

                If rs("fac_num_int") = "." Then
                    txtIntNumber.Text = ""
                Else
                    txtIntNumber.Text = rs("fac_num_int")
                End If

                txtColony.Text = rs("fac_colonia")
                txtCountry.Text = rs("fac_pais")
                cmbStates.SelectedValue = rs("fac_estadoid")
                txtTownship.Text = rs("fac_municipio")
                'txtZipCode.Text = rs("fac_cp")
                txtZipCod.Entries.Add(New AutoCompleteBoxEntry(rs("fac_cp"), rs("fac_cp")))
                'txtClienteProspecto.Entries.Clear()
                LoadDatosCodigoPostal(txtZipCod)
                txtRFC.Text = rs("fac_rfc")
                lblLogoName.Text = rs("logo")
                lblLogoName2.Text = rs("logo_formato")
                txtExpedidoLinea1.Text = rs("expedicionLinea1")
                txtExpedidoLinea2.Text = rs("expedicionLinea2")
                txtExpedidoLinea3.Text = rs("expedicionLinea3")
                email_from.Text = rs("email_from")
                email_smtp_server.Text = rs("email_smtp_server")
                email_smtp_username.Text = rs("email_smtp_username")
                email_smtp_port.Text = rs("email_smtp_port")
                email_smtp_password.Text = rs("email_smtp_password")
                regimenid.SelectedValue = rs("regimenid")
                If rs("porcentaje") > 0 Then
                    porcentaje.Text = rs("porcentaje")
                End If
                Dim ObjData As New DataControl
                ObjData.Catalogo(plantillaid, "select id, nombre from tblPlantilla order by nombre", rs("plantillaid"))
                ObjData = Nothing
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

#End Region

#Region "Save Data"

    Protected Sub btnSaveData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveData.Click

        Dim intNumber As String = txtIntNumber.Text
        Dim newFileName As String = ""
        Dim newFileName2 As String = ""
        Dim newFileName3 As String = ""
        Dim fac_cp As String = ""

        If intNumber = "" Then

            intNumber = "."

        End If

        If ((RadUpload1.UploadedFiles.Count = 0) And (lblLogoName.Text <> "")) Then

            newFileName = lblLogoName.Text

        Else

            For Each validFile As UploadedFile In RadUpload1.UploadedFiles

                newFileName = validFile.GetName()

                validFile.SaveAs(Server.MapPath("~/portalcfd/logos/") + newFileName)

            Next

        End If

        If ((RadUpload2.UploadedFiles.Count = 0) And (lblLogoName2.Text <> "")) Then

            newFileName2 = lblLogoName2.Text

        Else

            For Each validFile As UploadedFile In RadUpload2.UploadedFiles

                newFileName2 = validFile.GetName()

                validFile.SaveAs(Server.MapPath("~/portalcfd/logos/") + newFileName2)

            Next

        End If

        fac_cp = Replace(txtZipCod.Text.Trim, ";", "")

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try
            Dim cmd As New SqlCommand("EXEC pCliente @cmd=4, @clienteid='" & Session("clienteid") & "', @razonsocial='" & txtSocialReason.Text & "', @fac_calle='" & txtStreet.Text & "', @fac_num_int='" & intNumber & "', @fac_num_ext='" & txtExtNumber.Text & "', @fac_colonia='" & txtColony.Text & "',  @fac_pais='" & txtCountry.Text & "', @fac_municipio='" & txtTownship.Text & "', @fac_estadoid='" & cmbStates.SelectedValue & "', @fac_cp='" & fac_cp.ToString & "', @fac_rfc='" & txtRFC.Text & "', @email_contacto='" & txtEmailContact.Text & "', @logo='" & newFileName & "', @logo_formato='" & newFileName2 & "', @logo_formato_2='" & newFileName3 & "', @expedicionLinea1='" & txtExpedidoLinea1.Text & "', @expedicionLinea2='" & txtExpedidoLinea2.Text & "', @expedicionLinea3='" & txtExpedidoLinea3.Text & "', @email_from='" & email_from.Text & "', @email_smtp_server='" & email_smtp_server.Text & "', @email_smtp_username='" & email_smtp_username.Text & "', @email_smtp_password='" & email_smtp_password.Text & "', @email_smtp_port='" & email_smtp_port.Text & "', @porcentaje='" & porcentaje.Text & "', @plantillaid='" & plantillaid.SelectedValue.ToString & "', @regimenid='" & regimenid.SelectedValue & "'", conn)

            conn.Open()

            cmd.ExecuteReader()

            conn.Close()
            conn.Dispose()

            Response.Redirect("~/portalcfd/Datos.aspx")

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

#End Region

End Class
