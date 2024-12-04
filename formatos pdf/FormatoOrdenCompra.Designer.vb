Partial Class FormatoOrdenCompra
    
    'NOTE: The following procedure is required by the telerik Reporting Designer
    'It can be modified using the telerik Reporting Designer.  
    'Do not modify it using the code editor.
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormatoOrdenCompra))
        Dim ReportParameter1 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter
        Me.pageHeaderSection1 = New Telerik.Reporting.PageHeaderSection
        Me.PictureBox1 = New Telerik.Reporting.PictureBox
        Me.lblOrdenCompra = New Telerik.Reporting.TextBox
        Me.lblFecha = New Telerik.Reporting.TextBox
        Me.lblProveedor = New Telerik.Reporting.TextBox
        Me.lblCodigo = New Telerik.Reporting.TextBox
        Me.txtOrdenCompra = New Telerik.Reporting.TextBox
        Me.txtFecha = New Telerik.Reporting.TextBox
        Me.txtProveedor = New Telerik.Reporting.TextBox
        Me.lblCantidad = New Telerik.Reporting.TextBox
        Me.lblDescripcion = New Telerik.Reporting.TextBox
        Me.TextBox4 = New Telerik.Reporting.TextBox
        Me.lblCosto = New Telerik.Reporting.TextBox
        Me.TextBox5 = New Telerik.Reporting.TextBox
        Me.detail = New Telerik.Reporting.DetailSection
        Me.TextBox8 = New Telerik.Reporting.TextBox
        Me.TextBox6 = New Telerik.Reporting.TextBox
        Me.TextBox10 = New Telerik.Reporting.TextBox
        Me.TextBox7 = New Telerik.Reporting.TextBox
        Me.TextBox9 = New Telerik.Reporting.TextBox
        Me.TextBox11 = New Telerik.Reporting.TextBox
        Me.pageFooterSection1 = New Telerik.Reporting.PageFooterSection
        Me.panelSelloDigitalSAT = New Telerik.Reporting.Panel
        Me.TextBox12 = New Telerik.Reporting.TextBox
        Me.txtCostoTotal = New Telerik.Reporting.TextBox
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'pageHeaderSection1
        '
        Me.pageHeaderSection1.Height = New Telerik.Reporting.Drawing.Unit(6.1999998092651367, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.pageHeaderSection1.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.PictureBox1, Me.lblOrdenCompra, Me.lblFecha, Me.lblProveedor, Me.lblCodigo, Me.txtOrdenCompra, Me.txtFecha, Me.txtProveedor, Me.lblCantidad, Me.lblDescripcion, Me.TextBox4, Me.lblCosto, Me.TextBox5})
        Me.pageHeaderSection1.Name = "pageHeaderSection1"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.000039418537198798731, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.000039378803194267675, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.PictureBox1.MimeType = "image/jpeg"
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.7061252593994141, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.4172835350036621, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.PictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch
        Me.PictureBox1.Value = CType(resources.GetObject("PictureBox1.Value"), Object)
        '
        'lblOrdenCompra
        '
        Me.lblOrdenCompra.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.5354331731796265, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.lblOrdenCompra.Name = "lblOrdenCompra"
        Me.lblOrdenCompra.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.8205411434173584, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19685037434101105, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.lblOrdenCompra.Style.Font.Bold = True
        Me.lblOrdenCompra.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.lblOrdenCompra.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblOrdenCompra.Value = "Orden No.:"
        '
        'lblFecha
        '
        Me.lblFecha.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.7323623895645142, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.8205411434173584, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19685037434101105, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.lblFecha.Style.Font.Bold = True
        Me.lblFecha.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.lblFecha.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblFecha.Value = "Fecha:"
        '
        'lblProveedor
        '
        Me.lblProveedor.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.9292917251586914, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.lblProveedor.Name = "lblProveedor"
        Me.lblProveedor.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.8205411434173584, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19685037434101105, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.lblProveedor.Style.Font.Bold = True
        Me.lblProveedor.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.lblProveedor.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblProveedor.Value = "Proveedor:"
        '
        'lblCodigo
        '
        Me.lblCodigo.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(5.5999999046325684, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.299100399017334, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.5999000072479248, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.lblCodigo.Style.BackgroundColor = System.Drawing.Color.Black
        Me.lblCodigo.Style.Color = System.Drawing.Color.White
        Me.lblCodigo.Style.Font.Bold = False
        Me.lblCodigo.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.lblCodigo.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblCodigo.Value = "Código"
        '
        'txtOrdenCompra
        '
        Me.txtOrdenCompra.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.82061988115310669, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.535433292388916, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtOrdenCompra.Name = "txtOrdenCompra"
        Me.txtOrdenCompra.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.91764760017395, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19685037434101105, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtOrdenCompra.Style.Font.Bold = False
        Me.txtOrdenCompra.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.txtOrdenCompra.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.txtOrdenCompra.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.txtOrdenCompra.Value = ""
        '
        'txtFecha
        '
        Me.txtFecha.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.82061988115310669, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.7323623895645142, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.91764760017395, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19685037434101105, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtFecha.Style.Font.Bold = False
        Me.txtFecha.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.txtFecha.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.txtFecha.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.txtFecha.Value = ""
        '
        'txtProveedor
        '
        Me.txtProveedor.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.82061988115310669, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.9292917251586914, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtProveedor.Name = "txtProveedor"
        Me.txtProveedor.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.91764760017395, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19685037434101105, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtProveedor.Style.Font.Bold = False
        Me.txtProveedor.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.txtProveedor.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.txtProveedor.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.txtProveedor.Value = ""
        '
        'lblCantidad
        '
        Me.lblCantidad.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(2.2993004322052, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(5.6000003814697266, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(1.9998998641967773, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.5999000072479248, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.lblCantidad.Style.BackgroundColor = System.Drawing.Color.Black
        Me.lblCantidad.Style.Color = System.Drawing.Color.White
        Me.lblCantidad.Style.Font.Bold = False
        Me.lblCantidad.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.lblCantidad.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblCantidad.Value = "Cantidad"
        '
        'lblDescripcion
        '
        Me.lblDescripcion.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(6.5996003150939941, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(5.6000003814697266, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(8.9832954406738281, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.5999000072479248, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.lblDescripcion.Style.BackgroundColor = System.Drawing.Color.Black
        Me.lblDescripcion.Style.Color = System.Drawing.Color.White
        Me.lblDescripcion.Style.Font.Bold = False
        Me.lblDescripcion.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.lblDescripcion.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblDescripcion.Value = "Descripción"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(4.2994003295898437, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(5.599998950958252, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.2999999523162842, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.60000002384185791, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox4.Style.BackgroundColor = System.Drawing.Color.Black
        Me.TextBox4.Style.Color = System.Drawing.Color.White
        Me.TextBox4.Style.Font.Bold = False
        Me.TextBox4.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox4.Value = "U. Medida"
        '
        'lblCosto
        '
        Me.lblCosto.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(15.583097457885742, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(5.599998950958252, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.lblCosto.Name = "lblCosto"
        Me.lblCosto.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.60000002384185791, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.lblCosto.Style.BackgroundColor = System.Drawing.Color.Black
        Me.lblCosto.Style.Color = System.Drawing.Color.White
        Me.lblCosto.Style.Font.Bold = False
        Me.lblCosto.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.lblCosto.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.lblCosto.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.lblCosto.Value = "Costo"
        '
        'TextBox5
        '
        Me.TextBox5.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(17.583297729492188, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(5.6000003814697266, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.60000002384185791, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox5.Style.BackgroundColor = System.Drawing.Color.Black
        Me.TextBox5.Style.Color = System.Drawing.Color.White
        Me.TextBox5.Style.Font.Bold = False
        Me.TextBox5.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.TextBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox5.Value = "Total"
        '
        'detail
        '
        Me.detail.Height = New Telerik.Reporting.Drawing.Unit(0.50029969215393066, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.detail.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.TextBox8, Me.TextBox6, Me.TextBox10, Me.TextBox7, Me.TextBox9, Me.TextBox11})
        Me.detail.Name = "detail"
        '
        'TextBox8
        '
        Me.TextBox8.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.00010012308484874666, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.2991006374359131, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox8.Style.BackgroundColor = System.Drawing.Color.White
        Me.TextBox8.Style.Color = System.Drawing.Color.Black
        Me.TextBox8.Style.Font.Bold = False
        Me.TextBox8.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox8.Value = "=codigo"
        '
        'TextBox6
        '
        Me.TextBox6.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(2.2993004322052, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox6.Name = "TextBox5"
        Me.TextBox6.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(1.9997000694274902, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.50010043382644653, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox6.Style.BackgroundColor = System.Drawing.Color.White
        Me.TextBox6.Style.Color = System.Drawing.Color.Black
        Me.TextBox6.Style.Font.Bold = False
        Me.TextBox6.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox6.Value = "=cantidad"
        '
        'TextBox10
        '
        Me.TextBox10.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(4.2994003295898437, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.00019916957535315305, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.299999475479126, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.50010043382644653, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox10.Style.BackgroundColor = System.Drawing.Color.White
        Me.TextBox10.Style.Color = System.Drawing.Color.Black
        Me.TextBox10.Style.Font.Bold = False
        Me.TextBox10.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox10.Value = "=unidad"
        '
        'TextBox7
        '
        Me.TextBox7.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(6.5996003150939941, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.00029929267475381494, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(8.9832963943481445, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.50000029802322388, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox7.Style.BackgroundColor = System.Drawing.Color.White
        Me.TextBox7.Style.Color = System.Drawing.Color.Black
        Me.TextBox7.Style.Font.Bold = False
        Me.TextBox7.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox7.Value = "=descripcion"
        '
        'TextBox9
        '
        Me.TextBox9.Format = "{0:C2}"
        Me.TextBox9.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(15.583097457885742, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.00029929267475381494, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.0000007152557373, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.50000029802322388, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox9.Style.BackgroundColor = System.Drawing.Color.White
        Me.TextBox9.Style.Color = System.Drawing.Color.Black
        Me.TextBox9.Style.Font.Bold = False
        Me.TextBox9.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.TextBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox9.Value = "=costo_estandar"
        '
        'TextBox11
        '
        Me.TextBox11.Format = "{0:C2}"
        Me.TextBox11.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(17.583297729492188, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(2.0000007152557373, Telerik.Reporting.Drawing.UnitType.Cm), New Telerik.Reporting.Drawing.Unit(0.50000029802322388, Telerik.Reporting.Drawing.UnitType.Cm))
        Me.TextBox11.Style.BackgroundColor = System.Drawing.Color.White
        Me.TextBox11.Style.Color = System.Drawing.Color.Black
        Me.TextBox11.Style.Font.Bold = False
        Me.TextBox11.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.TextBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox11.Value = "=subtotal"
        '
        'pageFooterSection1
        '
        Me.pageFooterSection1.Height = New Telerik.Reporting.Drawing.Unit(1.0997010469436646, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.pageFooterSection1.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.panelSelloDigitalSAT})
        Me.pageFooterSection1.Name = "pageFooterSection1"
        '
        'panelSelloDigitalSAT
        '
        Me.panelSelloDigitalSAT.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.TextBox12, Me.txtCostoTotal})
        Me.panelSelloDigitalSAT.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(5.8953962326049805, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.039251964539289474, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.panelSelloDigitalSAT.Name = "panelSelloDigitalSAT"
        Me.panelSelloDigitalSAT.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(1.8107689619064331, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.3543306291103363, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.panelSelloDigitalSAT.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid
        Me.panelSelloDigitalSAT.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Point)
        '
        'TextBox12
        '
        Me.TextBox12.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.043732326477766037, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.078740119934082031, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.8205411434173584, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19685037434101105, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox12.Style.Font.Bold = True
        Me.TextBox12.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.TextBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox12.Value = "Total"
        '
        'txtCostoTotal
        '
        Me.txtCostoTotal.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.86435192823410034, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.078740119934082031, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtCostoTotal.Name = "txtCostoTotal"
        Me.txtCostoTotal.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(0.94641703367233276, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19685037434101105, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtCostoTotal.Style.Font.Bold = True
        Me.txtCostoTotal.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point)
        Me.txtCostoTotal.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.txtCostoTotal.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.txtCostoTotal.Value = ""
        '
        'FormatoOrdenCompra
        '
        Me.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.pageHeaderSection1, Me.detail, Me.pageFooterSection1})
        Me.PageSettings.Landscape = False
        Me.PageSettings.Margins.Bottom = New Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageSettings.Margins.Left = New Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageSettings.Margins.Right = New Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageSettings.Margins.Top = New Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter
        ReportParameter1.AllowNull = True
        ReportParameter1.Name = "pIdOrdenCompra"
        Me.ReportParameters.Add(ReportParameter1)
        Me.Style.BackgroundColor = System.Drawing.Color.White
        Me.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Inch
        Me.Width = New Telerik.Reporting.Drawing.Unit(7.7100000381469727, Telerik.Reporting.Drawing.UnitType.Inch)
        Me.Name = "FormatoOrdenCompra"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents pageHeaderSection1 As Telerik.Reporting.PageHeaderSection
    Friend WithEvents detail As Telerik.Reporting.DetailSection
    Friend WithEvents pageFooterSection1 As Telerik.Reporting.PageFooterSection
    Friend WithEvents PictureBox1 As Telerik.Reporting.PictureBox
    Friend WithEvents lblOrdenCompra As Telerik.Reporting.TextBox
    Friend WithEvents lblFecha As Telerik.Reporting.TextBox
    Friend WithEvents lblProveedor As Telerik.Reporting.TextBox
    Friend WithEvents lblCodigo As Telerik.Reporting.TextBox
    Friend WithEvents txtOrdenCompra As Telerik.Reporting.TextBox
    Friend WithEvents txtFecha As Telerik.Reporting.TextBox
    Friend WithEvents txtProveedor As Telerik.Reporting.TextBox
    Friend WithEvents lblCantidad As Telerik.Reporting.TextBox
    Friend WithEvents lblDescripcion As Telerik.Reporting.TextBox
    Friend WithEvents TextBox4 As Telerik.Reporting.TextBox
    Friend WithEvents lblCosto As Telerik.Reporting.TextBox
    Friend WithEvents TextBox5 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox8 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox6 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox10 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox7 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox9 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox11 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox12 As Telerik.Reporting.TextBox
    Friend WithEvents txtCostoTotal As Telerik.Reporting.TextBox
    Friend WithEvents panelSelloDigitalSAT As Telerik.Reporting.Panel
End Class