
Partial Class portalcfd_testXML
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub

    Protected Sub btnValidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        Dim archivoxml As String = ""
        Dim cadena As String = ""
        Dim sello As String = ""
        Dim certificado As String = ""
        Dim noAprobacion As String = ""
        Dim anoAprobacion As String = ""


        archivoxml = Server.MapPath("~/portalcfd/cfd_storage/iu_sig_A5819.xml")
        '
        '
        cadena = Global.FirmaSAT.Sat.MakePipeStringFromXml(archivoxml)
        cadena = Replace(cadena, "Ã³", "ó")
        cadena = Replace(cadena, "Ã¡", "á")
        cadena = Replace(cadena, "Ã©", "é")

        sello = Global.FirmaSAT.Sat.GetXmlAttribute(archivoxml, "sello", "Comprobante")
        certificado = Global.FirmaSAT.Sat.GetXmlAttribute(archivoxml, "noCertificado", "Comprobante")
        noAprobacion = Global.FirmaSAT.Sat.GetXmlAttribute(archivoxml, "noAprobacion", "Comprobante")
        anoAprobacion = Global.FirmaSAT.Sat.GetXmlAttribute(archivoxml, "anoAprobacion", "Comprobante")

        lblSello.Text = sello
        lblCadena.Text = cadena
        lblCertificado.Text = certificado
        lblNoAprobacion.Text = noAprobacion
        lblAnioAprobacion.Text = anoAprobacion
    End Sub
End Class
