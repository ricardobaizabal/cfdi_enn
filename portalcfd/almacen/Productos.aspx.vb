Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class almacen_portalcfd_Productos
    Inherits System.Web.UI.Page

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then

            '''''''''''''''''''
            'Fieldsets Legends'
            '''''''''''''''''''

            lblProductsListLegend.Text = Resources.Resource.lblProductsListLegend
            lblProductEditLegend.Text = Resources.Resource.lblProductEditLegend

            ''''''''''''''
            'Label Titles'
            ''''''''''''''

            lblCode.Text = Resources.Resource.lblCode
            lblUnit.Text = Resources.Resource.lblUnit
            lblUnitaryPrice.Text = "Precio Unit. 1"
            lblUnitaryPrice2.Text = "Precio Unit. 2"
            lblUnitaryPrice3.Text = "Precio Unit. 3"

            lblDescription.Text = Resources.Resource.lblDescription

            '''''''''''''''''''
            'Validators Titles'
            '''''''''''''''''''

            RequiredFieldValidator1.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator2.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator3.Text = Resources.Resource.validatorMessage

            ''''''''''''''''
            'Buttons Titles'
            ''''''''''''''''

            btnAddProduct.Text = Resources.Resource.btnAddProduct
            btnSaveProduct.Text = Resources.Resource.btnSave
            btnCancel.Text = Resources.Resource.btnCancel
            '
            '
            '
            '
            Dim objCat As New DataControl
            objCat.Catalogo(tasaid, "select id, nombre from tblTasa order by id", 0)
            objCat.Catalogo(monedaid, "select id, nombre from tblMoneda order by id", 0)
            objCat.Catalogo(proveedorid, "select id, razonsocial from tblMisProveedores order by razonsocial", 0)
            objCat = Nothing
            '
            chkInventariableBit.Checked = True
            '
        End If

    End Sub

#End Region

#Region "Load List Of Products"

    Function GetProducts() As DataSet
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pMisProductos  @cmd=1, @txtSearch='" & txtSearch.Text & "'", conn)
        Dim ds As DataSet = New DataSet
        Try
            conn.Open()
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        Return ds
    End Function

#End Region

#Region "Telerik Grid Products Loading Events"

    Protected Sub productslist_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles productslist.NeedDataSource

        If Not e.IsFromDetailTable Then

            productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
            productslist.DataSource = GetProducts()

        End If

    End Sub

#End Region

#Region "Telerik Grid Language Modification(Spanish)"

    Protected Sub productslist_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles productslist.Init

        productslist.PagerStyle.NextPagesToolTip = "Ver mas"
        productslist.PagerStyle.NextPageToolTip = "Siguiente"
        productslist.PagerStyle.PrevPagesToolTip = "Ver más"
        productslist.PagerStyle.PrevPageToolTip = "Atrás"
        productslist.PagerStyle.LastPageToolTip = "Última Página"
        productslist.PagerStyle.FirstPageToolTip = "Primera Página"
        productslist.PagerStyle.PagerTextFormat = "{4}    Página {0} de {1}, Registros {2} al {3} de {5}"
        productslist.SortingSettings.SortToolTip = "Ordernar"
        productslist.SortingSettings.SortedAscToolTip = "Ordenar Asc"
        productslist.SortingSettings.SortedDescToolTip = "Ordenar Desc"


        Dim menu As Telerik.Web.UI.GridFilterMenu = productslist.FilterMenu
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

        productslist.GroupingSettings.CaseSensitive = False

        Dim Menu As Telerik.Web.UI.GridFilterMenu = productslist.FilterMenu
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

#Region "Telerik Grid Products Editing & Deleting Events"

    Protected Sub productslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles productslist.ItemDataBound

        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Products" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('" & Resources.Resource.ProductsDeleteConfirmationMessage & "');")

            End If

        End If

    End Sub

    Protected Sub productslist_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles productslist.ItemCommand

        Select Case e.CommandName

            Case "cmdEdit"
                EditProduct(e.CommandArgument)

            Case "cmdDelete"
                DeleteProduct(e.CommandArgument)

        End Select

    End Sub

    Private Sub DeleteProduct(ByVal id As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pMisProductos  @cmd='2', @productoId ='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelProductRegistration.Visible = False

            productslist.DataSource = GetProducts()
            productslist.DataBind()

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

    Private Sub EditProduct(ByVal id As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        'Try

        Dim cmd As New SqlCommand("EXEC pMisProductos @cmd='4', @productoid='" & id & "'", conn)

        conn.Open()

        Dim rs As SqlDataReader
        rs = cmd.ExecuteReader()

        If rs.Read Then

            txtCode.Text = rs("codigo")
            txtUnit.Text = rs("unidad")
            txtUnitaryPrice.Text = rs("unitario")
            txtUnitaryPrice2.Text = rs("unitario2")
            txtUnitaryPrice3.Text = rs("unitario3")
            txtDescription.Text = rs("descripcion")
            '
            txtMinimo.Text = rs("minimo")
            txtMaximo.Text = rs("maximo")
            txtReorden.Text = rs("punto_reorden")
            txtCostoStd.Text = rs("costo_estandar")
            txtCompraMinima.Text = rs("compra_min")
            txtUso.Text = rs("uso")
            txtTiempoEntrega.Text = rs("tiempo_entrega")
            txtPresentacion.Text = rs("presentacion")
            txtTipoCambio.Text = rs("tipo_cambio_std")


            panelProductRegistration.Visible = True

            InsertOrUpdate.Value = 1
            ProductID.Value = id
            Dim objCat As New DataControl
            objCat.Catalogo(tasaid, "select id, nombre from tblTasa order by id", rs("tasaid"))
            objCat.Catalogo(monedaid, "select id, nombre from tblMoneda order by id", rs("monedaid"))
            objCat.Catalogo(proveedorid, "select id, razonsocial from tblMisProveedores order by razonsocial", rs("proveedorid"))
            objCat = Nothing

            'chkInventariableBit.Checked = rs("inventariableBit")
            'chkManiobraBit.Checked = rs("maniobraBit")

        End If

        'Catch ex As Exception

        'Finally

        conn.Close()
        conn.Dispose()

        'End Try

    End Sub

#End Region

#Region "Telerik Grid Products Column Names (From Resource File)"

    Protected Sub productslist_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles productslist.ItemCreated

        If TypeOf e.Item Is GridHeaderItem Then

            Dim header As GridHeaderItem = CType(e.Item, GridHeaderItem)

            If e.Item.OwnerTableView.Name = "Products" Then

                header("codigo").Text = Resources.Resource.gridColumnNameCode
                header("unidad").Text = Resources.Resource.gridColumnNameMeasureUnit
                header("descripcion").Text = Resources.Resource.gridColumnNameDescription
                header("unitario").Text = Resources.Resource.gridColumnNameUnitaryPrice
                header("unitario2").Text = Resources.Resource.gridColumnNameUnitaryPrice2
                header("unitario3").Text = Resources.Resource.gridColumnNameUnitaryPrice3

            End If

        End If

    End Sub

#End Region

#Region "Display Product Data Panel"

    Protected Sub btnAddProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddProduct.Click

        InsertOrUpdate.Value = 0

        txtCode.Text = ""
        txtUnit.Text = ""
        txtUnitaryPrice.Text = ""
        txtUnitaryPrice2.Text = ""
        txtUnitaryPrice3.Text = ""
        txtDescription.Text = ""

        panelProductRegistration.Visible = True

    End Sub

#End Region

#Region "Save Product"

    Protected Sub btnSaveClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveProduct.Click

        Dim inventariableBit As Integer = 0
        Dim maniobraBit As Integer = 0
        If chkInventariableBit.Checked = True Then
            inventariableBit = 1
        Else
            inventariableBit = 0
        End If
        '
        'If chkManiobraBit.Checked = True Then
        '    maniobraBit = 1
        'Else
        '    maniobraBit = 0
        'End If

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        'Try

        If InsertOrUpdate.Value = 0 Then

            Dim cmd As New SqlCommand("EXEC pMisProductos @cmd=3, @codigo='" & txtCode.Text & "', @unidad='" & txtUnit.Text & "', @unitario='" & txtUnitaryPrice.Text & "', @unitario2='" & txtUnitaryPrice2.Text & "', @unitario3='" & txtUnitaryPrice3.Text & "', @descripcion='" & txtDescription.Text & "', @tasaid='" & tasaid.SelectedValue.ToString & "', @maximo='" & txtMaximo.Text & "', @minimo='" & txtMinimo.Text & "', @punto_reorden='" & txtReorden.Text & "', @costo_estandar='" & txtCostoStd.Text & "', @compra_min='" & txtCompraMinima.Text & "', @uso='" & txtUso.Text & "', @tiempo_entrega='" & txtTiempoEntrega.Text & "', @presentacion='" & txtPresentacion.Text & "', @monedaid='" & monedaid.SelectedValue.ToString & "', @tipo_cambio_std='" & txtTipoCambio.Text & "', @proveedorId='" & proveedorid.SelectedValue.ToString & "', @inventariableBit='" & inventariableBit.ToString & "'", conn)

            conn.Open()

            cmd.ExecuteReader()

            panelProductRegistration.Visible = False

            productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
            productslist.DataSource = GetProducts()
            productslist.DataBind()

            conn.Close()
            conn.Dispose()

        Else

            Dim cmd As New SqlCommand("EXEC pMisProductos @cmd=5, @productoid='" & ProductID.Value & "', @codigo='" & txtCode.Text & "', @unidad='" & txtUnit.Text & "', @unitario='" & txtUnitaryPrice.Text & "', @unitario2='" & txtUnitaryPrice2.Text & "', @unitario3='" & txtUnitaryPrice3.Text & "', @descripcion='" & txtDescription.Text & "', @tasaid='" & tasaid.SelectedValue.ToString & "', @maximo='" & txtMaximo.Text & "', @minimo='" & txtMinimo.Text & "', @punto_reorden='" & txtReorden.Text & "', @costo_estandar='" & txtCostoStd.Text & "', @compra_min='" & txtCompraMinima.Text & "', @uso='" & txtUso.Text & "', @tiempo_entrega='" & txtTiempoEntrega.Text & "', @presentacion='" & txtPresentacion.Text & "', @monedaid='" & monedaid.SelectedValue.ToString & "', @tipo_cambio_std='" & txtTipoCambio.Text & "', @proveedorId='" & proveedorid.SelectedValue.ToString & "', @inventariableBit='" & inventariableBit.ToString & "'", conn)

            conn.Open()

            cmd.ExecuteReader()

            panelProductRegistration.Visible = False

            productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
            productslist.DataSource = GetProducts()
            productslist.DataBind()

            conn.Close()
            conn.Dispose()

        End If

        'Catch ex As Exception


        'Finally

        conn.Close()
        conn.Dispose()

        ' End Try

    End Sub

#End Region

#Region "Cancel Product (Save/Edit)"

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        InsertOrUpdate.Value = 0

        txtCode.Text = ""
        txtUnit.Text = ""
        txtUnitaryPrice.Text = ""
        txtDescription.Text = ""

        panelProductRegistration.Visible = False

    End Sub

#End Region

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
        productslist.DataSource = GetProducts()
        productslist.DataBind()
    End Sub

    Protected Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        txtSearch.Text = ""
        productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
        productslist.DataSource = GetProducts()
        productslist.DataBind()
    End Sub
End Class
