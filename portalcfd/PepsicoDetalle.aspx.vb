Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Telerik.Web.UI
Imports System.Globalization
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Object
Imports System.Xml.XPath.XPathItem
Imports System.Xml.XPath.XPathNavigator
Imports Cfdi32

Partial Public Class PepsicoDetalle
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            cfdid.Value = Request.QueryString("cfdid").ToString
            tipodocumentoid.Value = Request.QueryString("tipodocumentoid").ToString
            clienteid.Value = Request.QueryString("clienteid").ToString
            fhaini.Value = Request.QueryString("fhaini").ToString
            fhafin.Value = Request.QueryString("fhafin").ToString
            FolioUUID.Value = obtenerFolioUUID(Request.QueryString("cfdid").ToString)
        End If
    End Sub

    Private Function generarXmlDocAddenda(ByVal FacturaXML As Cfdi32.Comprobante) As XmlDocument
        Try
            Dim stream As New System.IO.MemoryStream()
            Dim xmlNameSpace As New XmlSerializerNamespaces()
            xmlNameSpace.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")
            xmlNameSpace.Add("cfdi", "http://www.sat.gob.mx/cfd/3")
            Dim xmlTextWriter As New XmlTextWriter(stream, Encoding.UTF8)
            xmlTextWriter.Formatting = Formatting.Indented
            Dim xs As New XmlSerializer(GetType(Cfdi32.Comprobante))
            xs.Serialize(xmlTextWriter, FacturaXML, xmlNameSpace)

            Dim doc As New System.Xml.XmlDocument()
            stream.Position = 0
            'stream.Seek(0, SeekOrigin.Begin)
            doc.Load(stream)

            Dim schemaLocation As XmlAttribute = doc.CreateAttribute("xsi", "schemaLocation", "http://www.w3.org/2001/XMLSchema-instance")
            schemaLocation.Value = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd"
            doc.DocumentElement.SetAttributeNode(schemaLocation)

            Dim schemaLocation2 As XmlAttribute = doc.CreateAttribute("xsi", "schemaLocation", "http://www.w3.org/2001/XMLSchema-instance")
            schemaLocation2.Value = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd"
            doc.DocumentElement.SetAttributeNode(schemaLocation2)

            xmlTextWriter.Close()

            Return doc
        Catch ex As Exception
            Throw New Exception("Error al generar XmlDocument, Error: " & ex.Message)
        End Try
    End Function

    Protected Sub btnGenerarAddenda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerarAddenda.Click

        btnGenerarAddenda.Enabled = False

        If cfdid.Value > 0 Then
            '
            '   Guarda addenda en la BD
            '
            Dim Objdata As New DataControl
            Objdata.RunSQLQuery("exec pAddendas @cmd=5, @cfdid='" & cfdid.Value.ToString & "', @idPedido='" & txtIdPedido.Text & "', @idSolicitudPago='" & txtIdSolicitudPago.Text & "', @referencia='" & txtReferencia.Text & "', @serie='" & serieCFD.Value.ToString & "', @folio='" & folioCFD.Value.ToString & "', @folioUUID='" & FolioUUID.Value.ToString & "', @tipoDoc='" & documentoid.SelectedValue.ToString & "', @idRecepcion='" & txtIdRecepcion.Text & "'")
            Objdata = Nothing

            Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
            Dim cmd As New SqlCommand("exec pAddendas @cmd=6, @id='" & cfdid.Value.ToString & "'", conn)
            Try

                conn.Open()

                Dim rs As SqlDataReader
                rs = cmd.ExecuteReader()

                If rs.Read Then
                    '
                    '   Genera nodos de XML
                    '
                    Dim adendaPCO As New uAddendas.AddendaPepsico.RequestCFD

                    adendaPCO.version = rs("version").ToString

                    adendaPCO.tipo = rs("nombre").ToString

                    If rs("IdPedido").ToString <> "" Then
                        adendaPCO.idPedido = (rs("IdPedido").ToString)
                    End If

                    If rs("IdSolicitudPago").ToString <> "" Then
                        adendaPCO.idSolicitudPago = rs("IdSolicitudPago").ToString
                    End If

                    Dim nodoDocumento As New uAddendas.AddendaPepsico.RequestCFDDocumento()
                    Dim nodoDocumentoItem As New List(Of uAddendas.AddendaPepsico.RequestCFDDocumento)

                    If rs("referencia").ToString <> "" Then
                        nodoDocumento.referencia = rs("referencia").ToString
                    End If

                    If rs("folioUUID").ToString <> "" Then
                        nodoDocumento.folioUUID = rs("folioUUID").ToString
                    End If

                    If rs("tipoDoc").ToString <> "" Then
                        nodoDocumento.tipoDoc = rs("tipoDoc").ToString
                    End If

                    nodoDocumentoItem.Add(nodoDocumento)
                    adendaPCO.Documento = nodoDocumentoItem.ToArray()

                    Dim nodoProveedor As New uAddendas.AddendaPepsico.RequestCFDProveedor()
                    Dim nodoProveedorItem As New List(Of uAddendas.AddendaPepsico.RequestCFDProveedor)

                    If rs("proveedorId").ToString <> "" Then
                        nodoProveedor.idProveedor = rs("proveedorId").ToString
                    End If

                    nodoProveedorItem.Add(nodoProveedor)
                    adendaPCO.Proveedor = nodoProveedorItem.ToArray()

                    Dim nodoRecepciones As New uAddendas.AddendaPepsico.RequestCFDRecepcionesRecepcion()
                    Dim nodoreRepcionesItem As New List(Of uAddendas.AddendaPepsico.RequestCFDRecepcionesRecepcion)

                    If rs("idRecepcion").ToString <> "" Then
                        nodoRecepciones.idRecepcion = rs("idRecepcion").ToString
                    End If

                    Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
                    Dim cmdF As New SqlCommand("select * from tblCFD_Partidas where cfdid='" & cfdid.Value.ToString & "'", connF)
                    Try

                        connF.Open()

                        Dim rsF As SqlDataReader
                        rsF = cmdF.ExecuteReader

                        Dim Concepto As New uAddendas.AddendaPepsico.RequestCFDRecepcionesRecepcionConcepto
                        Dim lstConceptos As New List(Of uAddendas.AddendaPepsico.RequestCFDRecepcionesRecepcionConcepto)

                        While rsF.Read
                            Concepto = New uAddendas.AddendaPepsico.RequestCFDRecepcionesRecepcionConcepto
                            Concepto.cantidad = rsF("cantidad").ToString
                            Concepto.unidad = rsF("unidad").ToString
                            Concepto.descripcion = rsF("descripcion").ToString
                            Concepto.valorUnitario = rsF("precio").ToString
                            Concepto.importe = rsF("importe").ToString
                            lstConceptos.Add(Concepto)
                        End While

                        nodoRecepciones.Concepto = lstConceptos.ToArray()
                        nodoreRepcionesItem.Add(nodoRecepciones)

                        adendaPCO.Recepciones = nodoreRepcionesItem.ToArray()

                    Catch ex As Exception
                        '
                    Finally
                        connF.Close()
                        connF.Dispose()
                        connF = Nothing
                    End Try

                    ''Inicializa el lector
                    Dim Lector As New Lector
                    '
                    Dim addenda As New Cfdi32.ComprobanteAddenda()

                    Dim resultadoAdenda As String = adendaPCO.Serialize()

                    Dim xmlDoc As System.Xml.XmlDocument = New XmlDocument()
                    xmlDoc.LoadXml(resultadoAdenda)

                    Dim elementoAddenda As XmlElement = xmlDoc.DocumentElement
                    addenda.Any = New XmlElement() {elementoAddenda}

                    FileCopier()

                    Dim rutaArchivoXML As String
                    rutaArchivoXML = RegresaUrlXML()

                    If rutaArchivoXML.ToString <> "" Then
                        Dim FacturaXML As Cfdi32.Comprobante
                        If Lector.LeerXML(rutaArchivoXML) Then
                            '
                            Lector.LeerVersion32()
                            FacturaXML = Lector.Cfdv32
                            FacturaXML.Addenda = addenda
                            generarXmlDocAddenda(FacturaXML).Save(rutaArchivoXML)

                            Dim Objdat As New DataControl
                            Objdat.RunSQLQuery("update tblCFD set addendaBit=1 where id='" & cfdid.Value.ToString & "'")
                            Objdat = Nothing

                        End If
                    End If

                End If

            Catch ex As Exception
                '
            Finally
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End Try

            documentoid.SelectedValue = 0
            txtIdPedido.Text = ""
            txtIdSolicitudPago.Text = ""
            txtIdRecepcion.Text = ""
            txtReferencia.Text = ""

            Page.ClientScript.RegisterClientScriptBlock([GetType](), "CloseScript", "redirectParentPage('CFD.aspx?tipodocumentoid=" + tipodocumentoid.Value.ToString + "&clienteid=" + clienteid.Value.ToString + "&fhaini=" + fhaini.Value.ToString + "&fhafin=" + fhafin.Value.ToString + "')", True)
        End If
    End Sub

    Private Function RegresaUrlXML() As String

        Dim URL As String = ""
        Dim serie As String = ""
        Dim folio As Long = 0
        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand("exec pCFD @cmd=18, @cfdid='" & cfdid.Value.ToString & "'", connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                serieCFD.Value = rs("serie").ToString
                folioCFD.Value = rs("folio").ToString
                serie = rs("serie").ToString
                folio = rs("folio").ToString
            End If
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        Dim FilePath = Server.MapPath("~/portalcfd/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml"

        If File.Exists(FilePath) Then
            URL = FilePath
        End If

        Return URL.ToString

    End Function

    Private Sub FileCopier()
        Dim FilePath = RegresaUrlXML()
        Dim Path = Server.MapPath("~/portalcfd/cfd_storage/").ToString

        If File.Exists(FilePath) Then
            Dim Source As String = FilePath.ToString
            Dim Destination As String = Path & "link_" & serieCFD.Value.ToString & folioCFD.Value.ToString & "_timbrado_OLD.xml"
            System.IO.File.Copy(Source, Destination)
        End If
    End Sub

    Public Function XML_Lee(ByVal URL As String, ByVal Clave As String, Optional ByVal Grupo As String = "", Optional ByVal SubGrupo As String = "") As String
        Try
            Dim ficheroXML As String = URL
            Dim doc As Xml.XmlDocument = New XmlDocument()
            If File.Exists(ficheroXML) Then
                doc.Load(ficheroXML)
            End If

            Dim oManager As New XmlNamespaceManager(New NameTable())
            oManager.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3")
            oManager.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")
            'Lectura del nodo principal
            Dim nodoPrincipal As Xml.XmlNode = doc.SelectSingleNode(Grupo, oManager)
            Dim nodoActual As Xml.XmlNode = nodoPrincipal.SelectSingleNode(SubGrupo, oManager)
            Return Convert.ToString(GetValor(nodoActual, Clave))
            'Return json
        Catch ex As Exception

            Return ""

        End Try
    End Function

    Private Function GetValor(ByVal nodoPadre As Xml.XmlNode, ByVal clave As String) As Object
        Dim valor As Xml.XmlNode = nodoPadre.Attributes(clave)
        Return valor.LastChild.Value
    End Function

    Public Function obtenerFolioUUID(ByVal cfdid As Long) As String
        Dim FolioUUID As String = ""
        Dim serie As String = ""
        Dim folio As Long = 0
        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand("exec pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                serie = rs("serie").ToString
                folio = rs("folio").ToString
            End If
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        Dim FilePath = Server.MapPath("~/portalcfd/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml"

        If File.Exists(FilePath) Then
            FolioUUID = XML_Lee(FilePath, "UUID", "cfdi:Comprobante", "cfdi:Complemento/tfd:TimbreFiscalDigital")
        End If

        Return FolioUUID

    End Function

    Protected Sub documentoid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles documentoid.SelectedIndexChanged

        If documentoid.SelectedValue = 2 Or documentoid.SelectedValue = 3 Then
            txtReferencia.Enabled = True
        Else
            txtReferencia.Enabled = False
        End If

    End Sub

End Class