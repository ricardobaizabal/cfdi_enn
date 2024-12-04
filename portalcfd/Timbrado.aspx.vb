Imports FirmaSAT.Sat
Imports System.Xml
Imports System.IO
Imports System.Xml.Serialization
Imports uCFDsLib.v3
Imports uCFDsLib

Partial Class portalcfd_Timbrado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim serie As String = "A"
        Dim folio As Long = 3569
        '
        Dim selloSAT As String = ""
        Dim noCertificadoSAT As String = ""
        Dim selloCFD As String = ""
        Dim fechaTimbrado As String = ""
        Dim UUID As String = ""
        Dim Version As String = ""
        '
        selloSAT = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_timbrado_" & serie.ToString & folio.ToString & ".xml", "selloSAT", "Timbre")
        noCertificadoSAT = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_timbrado_" & serie.ToString & folio.ToString & ".xml", "noCertificadoSAT", "Timbre")
        selloCFD = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_timbrado_" & serie.ToString & folio.ToString & ".xml", "selloCFD", "Timbre")
        fechaTimbrado = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_timbrado_" & serie.ToString & folio.ToString & ".xml", "FechaTimbrado", "Timbre")
        UUID = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_timbrado_" & serie.ToString & folio.ToString & ".xml", "UUID", "Timbre")
        Version = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_timbrado_" & serie.ToString & folio.ToString & ".xml", "version", "Timbre")
        '
        Response.Write("selloSAT: " & selloSAT & "<br />")
        Response.Write("noCertificadoSAT: " & noCertificadoSAT & "<br />")
        Response.Write("selloCFD: " & selloCFD & "<br />")
        Response.Write("FechaTimbrado: " & fechaTimbrado & "<br />")
        Response.Write("UUID: " & UUID & "<br />")
        Response.Write("version: " & Version & "<br />")
        '
        '
        '
        '
        'Crear el objeto timbre para asignar los valores de la respuesta PAC
        Dim timbre As New TimbreFiscalDigital
        timbre.FechaTimbrado = Convert.ToDateTime(fechaTimbrado)
        timbre.noCertificadoSAT = noCertificadoSAT
        timbre.selloCFD = selloCFD
        timbre.selloSAT = selloSAT
        timbre.UUID = UUID
        timbre.version = Version

        '
        '

        'Convertir el objeto TimbreFiscal a un nodo
        Dim stream As New System.IO.MemoryStream()
        Dim xmlNameSpace As New XmlSerializerNamespaces()
        xmlNameSpace.Add("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")        

        Dim xmlTextWriter As New XmlTextWriter(stream, Encoding.UTF8)
        xmlTextWriter.Formatting = Formatting.None

        Dim xs As New XmlSerializer(GetType(TimbreFiscalDigital))
        xs.Serialize(xmlTextWriter, timbre, xmlNameSpace)

        Dim doc As New System.Xml.XmlDocument()
        stream.Position = 0
        doc.Load(stream)

        xmlTextWriter.Close()

        doc.PreserveWhitespace = True


        doc.Save(Server.MapPath("cfd_storage") & "\" & "timbre_" & serie.ToString & folio.ToString & ".xml")

    End Sub


End Class
