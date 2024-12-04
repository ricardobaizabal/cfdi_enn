Imports System.Xml

Partial Class portalcfd_facturaxion_CreaUsuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If System.Configuration.ConfigurationManager.AppSettings("fx_pruebas") = 1 Then
                codigoProveedor.Text = "7F46BFC6811A33C87142DF03069039A030A6B483"
            Else
                codigoProveedor.Text = System.Configuration.ConfigurationManager.AppSettings("fx_codigousuarioproveedor_prod")
            End If
        End If
    End Sub
    Private Function Parametros() As String
        Dim xmlDocument As New XmlDocument()

        Dim declarationNode As XmlNode = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", String.Empty)

        xmlDocument.AppendChild(declarationNode)

        Dim root As XmlNode = xmlDocument.CreateElement("Parametros")
        xmlDocument.AppendChild(root)

        Dim attribute As XmlAttribute = root.OwnerDocument.CreateAttribute("Version")
        attribute.Value = "1.0"
        root.Attributes.Append(attribute)

        attribute = root.OwnerDocument.CreateAttribute("CodigoProveedor")
        attribute.Value = codigoProveedor.Text
        root.Attributes.Append(attribute)

        attribute = root.OwnerDocument.CreateAttribute("CodigoUsuarioProveedor")
        attribute.Value = codigoUsuarioProveedor.Text
        root.Attributes.Append(attribute)


        Return xmlDocument.InnerXml
    End Function

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Dim ServicioFX As New Administracion.AdministracionSoapClient
        Dim params As String = ""
        Dim codigoUsuarioProveedor As String = ""
        Dim codigoUsuario As String = ""
        Dim resultado As String = ""

        If System.Configuration.ConfigurationManager.AppSettings("fx_pruebas") = 1 Then
            ServicioFX.SolicitarCodigoUsuarioPrueba(Parametros, resultado)
        Else
            ServicioFX.SolicitarCodigoUsuario(Parametros, resultado)
        End If
        '
        '   Muestra el código de usuario obtenido
        '
        Call MostrarInformacion(resultado)
        '
    End Sub

    Private Sub MostrarInformacion(ByVal informacion As String)
        Dim xmlDocument As New XmlDocument()
        xmlDocument.LoadXml(informacion)
        Dim root As XmlElement = xmlDocument.DocumentElement

        If root.HasChildNodes Then
            Dim [error] As XmlElement = DirectCast(root.ChildNodes(0), XmlElement)
            lblNuevoCodigo.Text = [error].GetAttributeNode("Descripcion").Value
        Else
            lblNuevoCodigo.Text = root.GetAttributeNode("Codigo").Value
        End If
    End Sub

End Class
