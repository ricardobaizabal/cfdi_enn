Imports System
Imports System.Diagnostics
Imports System.Xml.Serialization
Imports System.Collections
Imports System.Xml.Schema
Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Collections.Generic

Namespace v32

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.sat.gob.mx/cfd/3"), _
     System.Xml.Serialization.XmlRootAttribute([Namespace]:="http://www.sat.gob.mx/cfd/3", IsNullable:=False)> _
    Partial Public Class Comprobante

        Private emisorField As ComprobanteEmisor

        Private receptorField As ComprobanteReceptor

        Private conceptosField As List(Of ComprobanteConcepto)

        Private impuestosField As ComprobanteImpuestos

        Private complementoField As ComprobanteComplemento

        Private addendaField As ComprobanteAddenda

        Private versionField As String

        Private serieField As String

        Private folioField As String

        Private fechaField As Date

        Private selloField As String

        Private formaDePagoField As String

        Private noCertificadoField As String

        Private certificadoField As String

        Private condicionesDePagoField As String

        Private subTotalField As Decimal

        Private descuentoField As Decimal

        Private descuentoFieldSpecified As Boolean

        Private motivoDescuentoField As String

        Private tipoCambioField As String

        Private monedaField As String

        Private totalField As Decimal

        Private tipoDeComprobanteField As ComprobanteTipoDeComprobante

        Private metodoDePagoField As String

        Private lugarExpedicionField As String

        Private numCtaPagoField As String

        Private folioFiscalOrigField As String

        Private serieFolioFiscalOrigField As String

        Private fechaFolioFiscalOrigField As Date

        Private fechaFolioFiscalOrigFieldSpecified As Boolean

        Private montoFolioFiscalOrigField As Decimal

        Private montoFolioFiscalOrigFieldSpecified As Boolean

        Private Shared sSerializer As System.Xml.Serialization.XmlSerializer

        Public Sub New()
            MyBase.New()
            Me.addendaField = New ComprobanteAddenda()
            Me.complementoField = New ComprobanteComplemento()
            Me.impuestosField = New ComprobanteImpuestos()
            Me.conceptosField = New List(Of ComprobanteConcepto)()
            Me.receptorField = New ComprobanteReceptor()
            Me.emisorField = New ComprobanteEmisor()
            Me.versionField = "3.2"
        End Sub

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)> _
        Public Property Emisor() As ComprobanteEmisor
            Get
                Return Me.emisorField
            End Get
            Set(value As ComprobanteEmisor)
                Me.emisorField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1)> _
        Public Property Receptor() As ComprobanteReceptor
            Get
                Return Me.receptorField
            End Get
            Set(value As ComprobanteReceptor)
                Me.receptorField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlArrayAttribute(Order:=2), _
         System.Xml.Serialization.XmlArrayItemAttribute("Concepto", IsNullable:=False)> _
        Public Property Conceptos() As List(Of ComprobanteConcepto)
            Get
                Return Me.conceptosField
            End Get
            Set(value As List(Of ComprobanteConcepto))
                Me.conceptosField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=3)> _
        Public Property Impuestos() As ComprobanteImpuestos
            Get
                Return Me.impuestosField
            End Get
            Set(value As ComprobanteImpuestos)
                Me.impuestosField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=4)> _
        Public Property Complemento() As ComprobanteComplemento
            Get
                Return Me.complementoField
            End Get
            Set(value As ComprobanteComplemento)
                Me.complementoField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=5)> _
        Public Property Addenda() As ComprobanteAddenda
            Get
                Return Me.addendaField
            End Get
            Set(value As ComprobanteAddenda)
                Me.addendaField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property version() As String
            Get
                Return Me.versionField
            End Get
            Set(value As String)
                Me.versionField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property serie() As String
            Get
                Return Me.serieField
            End Get
            Set(value As String)
                Me.serieField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property folio() As String
            Get
                Return Me.folioField
            End Get
            Set(value As String)
                Me.folioField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property fecha() As Date
            Get
                Return Me.fechaField
            End Get
            Set(value As Date)
                Me.fechaField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property sello() As String
            Get
                Return Me.selloField
            End Get
            Set(value As String)
                Me.selloField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property formaDePago() As String
            Get
                Return Me.formaDePagoField
            End Get
            Set(value As String)
                Me.formaDePagoField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property noCertificado() As String
            Get
                Return Me.noCertificadoField
            End Get
            Set(value As String)
                Me.noCertificadoField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property certificado() As String
            Get
                Return Me.certificadoField
            End Get
            Set(value As String)
                Me.certificadoField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property condicionesDePago() As String
            Get
                Return Me.condicionesDePagoField
            End Get
            Set(value As String)
                Me.condicionesDePagoField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property subTotal() As Decimal
            Get
                Return Me.subTotalField
            End Get
            Set(value As Decimal)
                Me.subTotalField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property descuento() As Decimal
            Get
                Return Me.descuentoField
            End Get
            Set(value As Decimal)
                Me.descuentoField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property descuentoSpecified() As Boolean
            Get
                Return Me.descuentoFieldSpecified
            End Get
            Set(value As Boolean)
                Me.descuentoFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property motivoDescuento() As String
            Get
                Return Me.motivoDescuentoField
            End Get
            Set(value As String)
                Me.motivoDescuentoField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property TipoCambio() As String
            Get
                Return Me.tipoCambioField
            End Get
            Set(value As String)
                Me.tipoCambioField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property Moneda() As String
            Get
                Return Me.monedaField
            End Get
            Set(value As String)
                Me.monedaField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property total() As Decimal
            Get
                Return Me.totalField
            End Get
            Set(value As Decimal)
                Me.totalField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property tipoDeComprobante() As ComprobanteTipoDeComprobante
            Get
                Return Me.tipoDeComprobanteField
            End Get
            Set(value As ComprobanteTipoDeComprobante)
                Me.tipoDeComprobanteField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property metodoDePago() As String
            Get
                Return Me.metodoDePagoField
            End Get
            Set(value As String)
                Me.metodoDePagoField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property LugarExpedicion() As String
            Get
                Return Me.lugarExpedicionField
            End Get
            Set(value As String)
                Me.lugarExpedicionField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property NumCtaPago() As String
            Get
                Return Me.numCtaPagoField
            End Get
            Set(value As String)
                Me.numCtaPagoField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property FolioFiscalOrig() As String
            Get
                Return Me.folioFiscalOrigField
            End Get
            Set(value As String)
                Me.folioFiscalOrigField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property SerieFolioFiscalOrig() As String
            Get
                Return Me.serieFolioFiscalOrigField
            End Get
            Set(value As String)
                Me.serieFolioFiscalOrigField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property FechaFolioFiscalOrig() As Date
            Get
                Return Me.fechaFolioFiscalOrigField
            End Get
            Set(value As Date)
                Me.fechaFolioFiscalOrigField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property FechaFolioFiscalOrigSpecified() As Boolean
            Get
                Return Me.fechaFolioFiscalOrigFieldSpecified
            End Get
            Set(value As Boolean)
                Me.fechaFolioFiscalOrigFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property MontoFolioFiscalOrig() As Decimal
            Get
                Return Me.montoFolioFiscalOrigField
            End Get
            Set(value As Decimal)
                Me.montoFolioFiscalOrigField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property MontoFolioFiscalOrigSpecified() As Boolean
            Get
                Return Me.montoFolioFiscalOrigFieldSpecified
            End Get
            Set(value As Boolean)
                Me.montoFolioFiscalOrigFieldSpecified = value
            End Set
        End Property

        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (sSerializer Is Nothing) Then
                    sSerializer = New System.Xml.Serialization.XmlSerializer(GetType(Comprobante))
                End If
                Return sSerializer
            End Get
        End Property

#Region "Serialize/Deserialize"
        '''<summary>
        '''Serializes current Comprobante object into an XML document
        '''</summary>
        '''<returns>string XML value</returns>
        Public Overridable Overloads Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter As System.Xml.XmlWriter = xmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter, Me)
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                streamReader = New System.IO.StreamReader(memoryStream)
                Return streamReader.ReadToEnd
            Finally
                If (Not (streamReader) Is Nothing) Then
                    streamReader.Dispose()
                End If
                If (Not (memoryStream) Is Nothing) Then
                    memoryStream.Dispose()
                End If
            End Try
        End Function

        Public Overridable Overloads Function Serialize() As String
            Return Serialize(Encoding.UTF8)
        End Function

        '''<summary>
        '''Deserializes workflow markup into an Comprobante object
        '''</summary>
        '''<param name="xml">string workflow markup to deserialize</param>
        '''<param name="obj">Output Comprobante object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As Comprobante, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, Comprobante)
            Try
                obj = Deserialize(xml)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As Comprobante) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String) As Comprobante
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return CType(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), Comprobante)
            Finally
                If (Not (stringReader) Is Nothing) Then
                    stringReader.Dispose()
                End If
            End Try
        End Function

        '''<summary>
        '''Serializes current Comprobante object into file
        '''</summary>
        '''<param name="fileName">full path of outupt xml file</param>
        '''<param name="exception">output Exception value if failed</param>
        '''<returns>true if can serialize and save into file; otherwise, false</returns>
        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByRef exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding)
            Dim streamWriter As System.IO.StreamWriter = Nothing
            Try
                Dim xmlString As String = Serialize(encoding)
                streamWriter = New System.IO.StreamWriter(fileName, False, encoding.UTF8)
                streamWriter.WriteLine(xmlString)
                streamWriter.Close()
            Finally
                If (Not (streamWriter) Is Nothing) Then
                    streamWriter.Dispose()
                End If
            End Try
        End Sub

        '''<summary>
        '''Deserializes xml markup from file into an Comprobante object
        '''</summary>
        '''<param name="fileName">string xml file to load and deserialize</param>
        '''<param name="obj">Output Comprobante object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef obj As Comprobante, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, Comprobante)
            Try
                obj = LoadFromFile(fileName, encoding)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As Comprobante, ByRef exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As Comprobante) As Boolean
            Dim exception As System.Exception = Nothing
            Return LoadFromFile(fileName, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String) As Comprobante
            Return LoadFromFile(fileName, Encoding.UTF8)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As Comprobante
            Dim file As System.IO.FileStream = Nothing
            Dim sr As System.IO.StreamReader = Nothing
            Try
                file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
                sr = New System.IO.StreamReader(file, encoding)
                Dim xmlString As String = sr.ReadToEnd
                sr.Close()
                file.Close()
                Return Deserialize(xmlString)
            Finally
                If (Not (file) Is Nothing) Then
                    file.Dispose()
                End If
                If (Not (sr) Is Nothing) Then
                    sr.Dispose()
                End If
            End Try
        End Function
#End Region
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.sat.gob.mx/cfd/3")> _
    Partial Public Class ComprobanteEmisor

        Private domicilioFiscalField As t_UbicacionFiscal

        Private expedidoEnField As t_Ubicacion

        Private regimenFiscalField As List(Of ComprobanteEmisorRegimenFiscal)

        Private rfcField As String

        Private nombreField As String

        Private Shared sSerializer As System.Xml.Serialization.XmlSerializer

        Public Sub New()
            MyBase.New()
            Me.regimenFiscalField = New List(Of ComprobanteEmisorRegimenFiscal)()
            Me.expedidoEnField = New t_Ubicacion()
            Me.domicilioFiscalField = New t_UbicacionFiscal()
        End Sub

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)> _
        Public Property DomicilioFiscal() As t_UbicacionFiscal
            Get
                Return Me.domicilioFiscalField
            End Get
            Set(value As t_UbicacionFiscal)
                Me.domicilioFiscalField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1)> _
        Public Property ExpedidoEn() As t_Ubicacion
            Get
                Return Me.expedidoEnField
            End Get
            Set(value As t_Ubicacion)
                Me.expedidoEnField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("RegimenFiscal", Order:=2)> _
        Public Property RegimenFiscal() As List(Of ComprobanteEmisorRegimenFiscal)
            Get
                Return Me.regimenFiscalField
            End Get
            Set(value As List(Of ComprobanteEmisorRegimenFiscal))
                Me.regimenFiscalField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property rfc() As String
            Get
                Return Me.rfcField
            End Get
            Set(value As String)
                Me.rfcField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property nombre() As String
            Get
                Return Me.nombreField
            End Get
            Set(value As String)
                Me.nombreField = value
            End Set
        End Property

        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (sSerializer Is Nothing) Then
                    sSerializer = New System.Xml.Serialization.XmlSerializer(GetType(ComprobanteEmisor))
                End If
                Return sSerializer
            End Get
        End Property

#Region "Serialize/Deserialize"
        '''<summary>
        '''Serializes current ComprobanteEmisor object into an XML document
        '''</summary>
        '''<returns>string XML value</returns>
        Public Overridable Overloads Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter As System.Xml.XmlWriter = xmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter, Me)
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                streamReader = New System.IO.StreamReader(memoryStream)
                Return streamReader.ReadToEnd
            Finally
                If (Not (streamReader) Is Nothing) Then
                    streamReader.Dispose()
                End If
                If (Not (memoryStream) Is Nothing) Then
                    memoryStream.Dispose()
                End If
            End Try
        End Function

        Public Overridable Overloads Function Serialize() As String
            Return Serialize(Encoding.UTF8)
        End Function

        '''<summary>
        '''Deserializes workflow markup into an ComprobanteEmisor object
        '''</summary>
        '''<param name="xml">string workflow markup to deserialize</param>
        '''<param name="obj">Output ComprobanteEmisor object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteEmisor, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteEmisor)
            Try
                obj = Deserialize(xml)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteEmisor) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String) As ComprobanteEmisor
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return CType(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), ComprobanteEmisor)
            Finally
                If (Not (stringReader) Is Nothing) Then
                    stringReader.Dispose()
                End If
            End Try
        End Function

        '''<summary>
        '''Serializes current ComprobanteEmisor object into file
        '''</summary>
        '''<param name="fileName">full path of outupt xml file</param>
        '''<param name="exception">output Exception value if failed</param>
        '''<returns>true if can serialize and save into file; otherwise, false</returns>
        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByRef exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding)
            Dim streamWriter As System.IO.StreamWriter = Nothing
            Try
                Dim xmlString As String = Serialize(encoding)
                streamWriter = New System.IO.StreamWriter(fileName, False, encoding.UTF8)
                streamWriter.WriteLine(xmlString)
                streamWriter.Close()
            Finally
                If (Not (streamWriter) Is Nothing) Then
                    streamWriter.Dispose()
                End If
            End Try
        End Sub

        '''<summary>
        '''Deserializes xml markup from file into an ComprobanteEmisor object
        '''</summary>
        '''<param name="fileName">string xml file to load and deserialize</param>
        '''<param name="obj">Output ComprobanteEmisor object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef obj As ComprobanteEmisor, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteEmisor)
            Try
                obj = LoadFromFile(fileName, encoding)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteEmisor, ByRef exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteEmisor) As Boolean
            Dim exception As System.Exception = Nothing
            Return LoadFromFile(fileName, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String) As ComprobanteEmisor
            Return LoadFromFile(fileName, Encoding.UTF8)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As ComprobanteEmisor
            Dim file As System.IO.FileStream = Nothing
            Dim sr As System.IO.StreamReader = Nothing
            Try
                file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
                sr = New System.IO.StreamReader(file, encoding)
                Dim xmlString As String = sr.ReadToEnd
                sr.Close()
                file.Close()
                Return Deserialize(xmlString)
            Finally
                If (Not (file) Is Nothing) Then
                    file.Dispose()
                End If
                If (Not (sr) Is Nothing) Then
                    sr.Dispose()
                End If
            End Try
        End Function
#End Region
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.sat.gob.mx/cfd/3"), _
     System.Xml.Serialization.XmlRootAttribute([Namespace]:="http://www.sat.gob.mx/cfd/3", IsNullable:=True)> _
    Partial Public Class t_UbicacionFiscal

        Private calleField As String

        Private noExteriorField As String

        Private noInteriorField As String

        Private coloniaField As String

        Private localidadField As String

        Private referenciaField As String

        Private municipioField As String

        Private estadoField As String

        Private paisField As String

        Private codigoPostalField As String

        Private Shared sSerializer As System.Xml.Serialization.XmlSerializer

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property calle() As String
            Get
                Return Me.calleField
            End Get
            Set(value As String)
                Me.calleField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property noExterior() As String
            Get
                Return Me.noExteriorField
            End Get
            Set(value As String)
                Me.noExteriorField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property noInterior() As String
            Get
                Return Me.noInteriorField
            End Get
            Set(value As String)
                Me.noInteriorField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property colonia() As String
            Get
                Return Me.coloniaField
            End Get
            Set(value As String)
                Me.coloniaField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property localidad() As String
            Get
                Return Me.localidadField
            End Get
            Set(value As String)
                Me.localidadField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property referencia() As String
            Get
                Return Me.referenciaField
            End Get
            Set(value As String)
                Me.referenciaField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property municipio() As String
            Get
                Return Me.municipioField
            End Get
            Set(value As String)
                Me.municipioField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property estado() As String
            Get
                Return Me.estadoField
            End Get
            Set(value As String)
                Me.estadoField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property pais() As String
            Get
                Return Me.paisField
            End Get
            Set(value As String)
                Me.paisField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property codigoPostal() As String
            Get
                Return Me.codigoPostalField
            End Get
            Set(value As String)
                Me.codigoPostalField = value
            End Set
        End Property

        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (sSerializer Is Nothing) Then
                    sSerializer = New System.Xml.Serialization.XmlSerializer(GetType(t_UbicacionFiscal))
                End If
                Return sSerializer
            End Get
        End Property

#Region "Serialize/Deserialize"
        '''<summary>
        '''Serializes current t_UbicacionFiscal object into an XML document
        '''</summary>
        '''<returns>string XML value</returns>
        Public Overridable Overloads Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter As System.Xml.XmlWriter = xmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter, Me)
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                streamReader = New System.IO.StreamReader(memoryStream)
                Return streamReader.ReadToEnd
            Finally
                If (Not (streamReader) Is Nothing) Then
                    streamReader.Dispose()
                End If
                If (Not (memoryStream) Is Nothing) Then
                    memoryStream.Dispose()
                End If
            End Try
        End Function

        Public Overridable Overloads Function Serialize() As String
            Return Serialize(Encoding.UTF8)
        End Function

        '''<summary>
        '''Deserializes workflow markup into an t_UbicacionFiscal object
        '''</summary>
        '''<param name="xml">string workflow markup to deserialize</param>
        '''<param name="obj">Output t_UbicacionFiscal object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As t_UbicacionFiscal, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, t_UbicacionFiscal)
            Try
                obj = Deserialize(xml)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As t_UbicacionFiscal) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String) As t_UbicacionFiscal
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return CType(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), t_UbicacionFiscal)
            Finally
                If (Not (stringReader) Is Nothing) Then
                    stringReader.Dispose()
                End If
            End Try
        End Function

        '''<summary>
        '''Serializes current t_UbicacionFiscal object into file
        '''</summary>
        '''<param name="fileName">full path of outupt xml file</param>
        '''<param name="exception">output Exception value if failed</param>
        '''<returns>true if can serialize and save into file; otherwise, false</returns>
        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByRef exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding)
            Dim streamWriter As System.IO.StreamWriter = Nothing
            Try
                Dim xmlString As String = Serialize(encoding)
                streamWriter = New System.IO.StreamWriter(fileName, False, encoding.UTF8)
                streamWriter.WriteLine(xmlString)
                streamWriter.Close()
            Finally
                If (Not (streamWriter) Is Nothing) Then
                    streamWriter.Dispose()
                End If
            End Try
        End Sub

        '''<summary>
        '''Deserializes xml markup from file into an t_UbicacionFiscal object
        '''</summary>
        '''<param name="fileName">string xml file to load and deserialize</param>
        '''<param name="obj">Output t_UbicacionFiscal object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef obj As t_UbicacionFiscal, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, t_UbicacionFiscal)
            Try
                obj = LoadFromFile(fileName, encoding)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As t_UbicacionFiscal, ByRef exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As t_UbicacionFiscal) As Boolean
            Dim exception As System.Exception = Nothing
            Return LoadFromFile(fileName, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String) As t_UbicacionFiscal
            Return LoadFromFile(fileName, Encoding.UTF8)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As t_UbicacionFiscal
            Dim file As System.IO.FileStream = Nothing
            Dim sr As System.IO.StreamReader = Nothing
            Try
                file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
                sr = New System.IO.StreamReader(file, encoding)
                Dim xmlString As String = sr.ReadToEnd
                sr.Close()
                file.Close()
                Return Deserialize(xmlString)
            Finally
                If (Not (file) Is Nothing) Then
                    file.Dispose()
                End If
                If (Not (sr) Is Nothing) Then
                    sr.Dispose()
                End If
            End Try
        End Function
#End Region
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.sat.gob.mx/cfd/3"), _
     System.Xml.Serialization.XmlRootAttribute([Namespace]:="http://www.sat.gob.mx/cfd/3", IsNullable:=True)> _
    Partial Public Class t_InformacionAduanera

        Private numeroField As String

        Private fechaField As Date

        Private aduanaField As String

        Private Shared sSerializer As System.Xml.Serialization.XmlSerializer

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property numero() As String
            Get
                Return Me.numeroField
            End Get
            Set(value As String)
                Me.numeroField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="date")> _
        Public Property fecha() As Date
            Get
                Return Me.fechaField
            End Get
            Set(value As Date)
                Me.fechaField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property aduana() As String
            Get
                Return Me.aduanaField
            End Get
            Set(value As String)
                Me.aduanaField = value
            End Set
        End Property

        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (sSerializer Is Nothing) Then
                    sSerializer = New System.Xml.Serialization.XmlSerializer(GetType(t_InformacionAduanera))
                End If
                Return sSerializer
            End Get
        End Property

#Region "Serialize/Deserialize"
        '''<summary>
        '''Serializes current t_InformacionAduanera object into an XML document
        '''</summary>
        '''<returns>string XML value</returns>
        Public Overridable Overloads Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter As System.Xml.XmlWriter = xmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter, Me)
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                streamReader = New System.IO.StreamReader(memoryStream)
                Return streamReader.ReadToEnd
            Finally
                If (Not (streamReader) Is Nothing) Then
                    streamReader.Dispose()
                End If
                If (Not (memoryStream) Is Nothing) Then
                    memoryStream.Dispose()
                End If
            End Try
        End Function

        Public Overridable Overloads Function Serialize() As String
            Return Serialize(Encoding.UTF8)
        End Function

        '''<summary>
        '''Deserializes workflow markup into an t_InformacionAduanera object
        '''</summary>
        '''<param name="xml">string workflow markup to deserialize</param>
        '''<param name="obj">Output t_InformacionAduanera object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As t_InformacionAduanera, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, t_InformacionAduanera)
            Try
                obj = Deserialize(xml)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As t_InformacionAduanera) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String) As t_InformacionAduanera
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return CType(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), t_InformacionAduanera)
            Finally
                If (Not (stringReader) Is Nothing) Then
                    stringReader.Dispose()
                End If
            End Try
        End Function

        '''<summary>
        '''Serializes current t_InformacionAduanera object into file
        '''</summary>
        '''<param name="fileName">full path of outupt xml file</param>
        '''<param name="exception">output Exception value if failed</param>
        '''<returns>true if can serialize and save into file; otherwise, false</returns>
        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByRef exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding)
            Dim streamWriter As System.IO.StreamWriter = Nothing
            Try
                Dim xmlString As String = Serialize(encoding)
                streamWriter = New System.IO.StreamWriter(fileName, False, encoding.UTF8)
                streamWriter.WriteLine(xmlString)
                streamWriter.Close()
            Finally
                If (Not (streamWriter) Is Nothing) Then
                    streamWriter.Dispose()
                End If
            End Try
        End Sub

        '''<summary>
        '''Deserializes xml markup from file into an t_InformacionAduanera object
        '''</summary>
        '''<param name="fileName">string xml file to load and deserialize</param>
        '''<param name="obj">Output t_InformacionAduanera object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef obj As t_InformacionAduanera, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, t_InformacionAduanera)
            Try
                obj = LoadFromFile(fileName, encoding)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As t_InformacionAduanera, ByRef exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As t_InformacionAduanera) As Boolean
            Dim exception As System.Exception = Nothing
            Return LoadFromFile(fileName, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String) As t_InformacionAduanera
            Return LoadFromFile(fileName, Encoding.UTF8)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As t_InformacionAduanera
            Dim file As System.IO.FileStream = Nothing
            Dim sr As System.IO.StreamReader = Nothing
            Try
                file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
                sr = New System.IO.StreamReader(file, encoding)
                Dim xmlString As String = sr.ReadToEnd
                sr.Close()
                file.Close()
                Return Deserialize(xmlString)
            Finally
                If (Not (file) Is Nothing) Then
                    file.Dispose()
                End If
                If (Not (sr) Is Nothing) Then
                    sr.Dispose()
                End If
            End Try
        End Function
#End Region
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.sat.gob.mx/cfd/3"), _
     System.Xml.Serialization.XmlRootAttribute([Namespace]:="http://www.sat.gob.mx/cfd/3", IsNullable:=True)> _
    Partial Public Class t_Ubicacion

        Private calleField As String

        Private noExteriorField As String

        Private noInteriorField As String

        Private coloniaField As String

        Private localidadField As String

        Private referenciaField As String

        Private municipioField As String

        Private estadoField As String

        Private paisField As String

        Private codigoPostalField As String

        Private Shared sSerializer As System.Xml.Serialization.XmlSerializer

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property calle() As String
            Get
                Return Me.calleField
            End Get
            Set(value As String)
                Me.calleField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property noExterior() As String
            Get
                Return Me.noExteriorField
            End Get
            Set(value As String)
                Me.noExteriorField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property noInterior() As String
            Get
                Return Me.noInteriorField
            End Get
            Set(value As String)
                Me.noInteriorField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property colonia() As String
            Get
                Return Me.coloniaField
            End Get
            Set(value As String)
                Me.coloniaField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property localidad() As String
            Get
                Return Me.localidadField
            End Get
            Set(value As String)
                Me.localidadField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property referencia() As String
            Get
                Return Me.referenciaField
            End Get
            Set(value As String)
                Me.referenciaField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property municipio() As String
            Get
                Return Me.municipioField
            End Get
            Set(value As String)
                Me.municipioField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property estado() As String
            Get
                Return Me.estadoField
            End Get
            Set(value As String)
                Me.estadoField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property pais() As String
            Get
                Return Me.paisField
            End Get
            Set(value As String)
                Me.paisField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property codigoPostal() As String
            Get
                Return Me.codigoPostalField
            End Get
            Set(value As String)
                Me.codigoPostalField = value
            End Set
        End Property

        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (sSerializer Is Nothing) Then
                    sSerializer = New System.Xml.Serialization.XmlSerializer(GetType(t_Ubicacion))
                End If
                Return sSerializer
            End Get
        End Property

#Region "Serialize/Deserialize"
        '''<summary>
        '''Serializes current t_Ubicacion object into an XML document
        '''</summary>
        '''<returns>string XML value</returns>
        Public Overridable Overloads Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter As System.Xml.XmlWriter = xmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter, Me)
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                streamReader = New System.IO.StreamReader(memoryStream)
                Return streamReader.ReadToEnd
            Finally
                If (Not (streamReader) Is Nothing) Then
                    streamReader.Dispose()
                End If
                If (Not (memoryStream) Is Nothing) Then
                    memoryStream.Dispose()
                End If
            End Try
        End Function

        Public Overridable Overloads Function Serialize() As String
            Return Serialize(Encoding.UTF8)
        End Function

        '''<summary>
        '''Deserializes workflow markup into an t_Ubicacion object
        '''</summary>
        '''<param name="xml">string workflow markup to deserialize</param>
        '''<param name="obj">Output t_Ubicacion object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As t_Ubicacion, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, t_Ubicacion)
            Try
                obj = Deserialize(xml)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As t_Ubicacion) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String) As t_Ubicacion
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return CType(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), t_Ubicacion)
            Finally
                If (Not (stringReader) Is Nothing) Then
                    stringReader.Dispose()
                End If
            End Try
        End Function

        '''<summary>
        '''Serializes current t_Ubicacion object into file
        '''</summary>
        '''<param name="fileName">full path of outupt xml file</param>
        '''<param name="exception">output Exception value if failed</param>
        '''<returns>true if can serialize and save into file; otherwise, false</returns>
        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByRef exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding)
            Dim streamWriter As System.IO.StreamWriter = Nothing
            Try
                Dim xmlString As String = Serialize(encoding)
                streamWriter = New System.IO.StreamWriter(fileName, False, encoding.UTF8)
                streamWriter.WriteLine(xmlString)
                streamWriter.Close()
            Finally
                If (Not (streamWriter) Is Nothing) Then
                    streamWriter.Dispose()
                End If
            End Try
        End Sub

        '''<summary>
        '''Deserializes xml markup from file into an t_Ubicacion object
        '''</summary>
        '''<param name="fileName">string xml file to load and deserialize</param>
        '''<param name="obj">Output t_Ubicacion object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef obj As t_Ubicacion, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, t_Ubicacion)
            Try
                obj = LoadFromFile(fileName, encoding)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As t_Ubicacion, ByRef exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As t_Ubicacion) As Boolean
            Dim exception As System.Exception = Nothing
            Return LoadFromFile(fileName, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String) As t_Ubicacion
            Return LoadFromFile(fileName, Encoding.UTF8)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As t_Ubicacion
            Dim file As System.IO.FileStream = Nothing
            Dim sr As System.IO.StreamReader = Nothing
            Try
                file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
                sr = New System.IO.StreamReader(file, encoding)
                Dim xmlString As String = sr.ReadToEnd
                sr.Close()
                file.Close()
                Return Deserialize(xmlString)
            Finally
                If (Not (file) Is Nothing) Then
                    file.Dispose()
                End If
                If (Not (sr) Is Nothing) Then
                    sr.Dispose()
                End If
            End Try
        End Function
#End Region
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.sat.gob.mx/cfd/3")> _
    Partial Public Class ComprobanteEmisorRegimenFiscal

        Private regimenField As String

        Private Shared sSerializer As System.Xml.Serialization.XmlSerializer

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property Regimen() As String
            Get
                Return Me.regimenField
            End Get
            Set(value As String)
                Me.regimenField = value
            End Set
        End Property

        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (sSerializer Is Nothing) Then
                    sSerializer = New System.Xml.Serialization.XmlSerializer(GetType(ComprobanteEmisorRegimenFiscal))
                End If
                Return sSerializer
            End Get
        End Property

#Region "Serialize/Deserialize"
        '''<summary>
        '''Serializes current ComprobanteEmisorRegimenFiscal object into an XML document
        '''</summary>
        '''<returns>string XML value</returns>
        Public Overridable Overloads Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter As System.Xml.XmlWriter = xmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter, Me)
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                streamReader = New System.IO.StreamReader(memoryStream)
                Return streamReader.ReadToEnd
            Finally
                If (Not (streamReader) Is Nothing) Then
                    streamReader.Dispose()
                End If
                If (Not (memoryStream) Is Nothing) Then
                    memoryStream.Dispose()
                End If
            End Try
        End Function

        Public Overridable Overloads Function Serialize() As String
            Return Serialize(Encoding.UTF8)
        End Function

        '''<summary>
        '''Deserializes workflow markup into an ComprobanteEmisorRegimenFiscal object
        '''</summary>
        '''<param name="xml">string workflow markup to deserialize</param>
        '''<param name="obj">Output ComprobanteEmisorRegimenFiscal object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteEmisorRegimenFiscal, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteEmisorRegimenFiscal)
            Try
                obj = Deserialize(xml)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteEmisorRegimenFiscal) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String) As ComprobanteEmisorRegimenFiscal
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return CType(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), ComprobanteEmisorRegimenFiscal)
            Finally
                If (Not (stringReader) Is Nothing) Then
                    stringReader.Dispose()
                End If
            End Try
        End Function

        '''<summary>
        '''Serializes current ComprobanteEmisorRegimenFiscal object into file
        '''</summary>
        '''<param name="fileName">full path of outupt xml file</param>
        '''<param name="exception">output Exception value if failed</param>
        '''<returns>true if can serialize and save into file; otherwise, false</returns>
        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByRef exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding)
            Dim streamWriter As System.IO.StreamWriter = Nothing
            Try
                Dim xmlString As String = Serialize(encoding)
                streamWriter = New System.IO.StreamWriter(fileName, False, encoding.UTF8)
                streamWriter.WriteLine(xmlString)
                streamWriter.Close()
            Finally
                If (Not (streamWriter) Is Nothing) Then
                    streamWriter.Dispose()
                End If
            End Try
        End Sub

        '''<summary>
        '''Deserializes xml markup from file into an ComprobanteEmisorRegimenFiscal object
        '''</summary>
        '''<param name="fileName">string xml file to load and deserialize</param>
        '''<param name="obj">Output ComprobanteEmisorRegimenFiscal object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef obj As ComprobanteEmisorRegimenFiscal, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteEmisorRegimenFiscal)
            Try
                obj = LoadFromFile(fileName, encoding)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteEmisorRegimenFiscal, ByRef exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteEmisorRegimenFiscal) As Boolean
            Dim exception As System.Exception = Nothing
            Return LoadFromFile(fileName, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String) As ComprobanteEmisorRegimenFiscal
            Return LoadFromFile(fileName, Encoding.UTF8)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As ComprobanteEmisorRegimenFiscal
            Dim file As System.IO.FileStream = Nothing
            Dim sr As System.IO.StreamReader = Nothing
            Try
                file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
                sr = New System.IO.StreamReader(file, encoding)
                Dim xmlString As String = sr.ReadToEnd
                sr.Close()
                file.Close()
                Return Deserialize(xmlString)
            Finally
                If (Not (file) Is Nothing) Then
                    file.Dispose()
                End If
                If (Not (sr) Is Nothing) Then
                    sr.Dispose()
                End If
            End Try
        End Function
#End Region
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.sat.gob.mx/cfd/3")> _
    Partial Public Class ComprobanteReceptor

        Private domicilioField As t_Ubicacion

        Private rfcField As String

        Private nombreField As String

        Private Shared sSerializer As System.Xml.Serialization.XmlSerializer

        Public Sub New()
            MyBase.New()
            Me.domicilioField = New t_Ubicacion()
        End Sub

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)> _
        Public Property Domicilio() As t_Ubicacion
            Get
                Return Me.domicilioField
            End Get
            Set(value As t_Ubicacion)
                Me.domicilioField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property rfc() As String
            Get
                Return Me.rfcField
            End Get
            Set(value As String)
                Me.rfcField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property nombre() As String
            Get
                Return Me.nombreField
            End Get
            Set(value As String)
                Me.nombreField = value
            End Set
        End Property

        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (sSerializer Is Nothing) Then
                    sSerializer = New System.Xml.Serialization.XmlSerializer(GetType(ComprobanteReceptor))
                End If
                Return sSerializer
            End Get
        End Property

#Region "Serialize/Deserialize"
        '''<summary>
        '''Serializes current ComprobanteReceptor object into an XML document
        '''</summary>
        '''<returns>string XML value</returns>
        Public Overridable Overloads Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter As System.Xml.XmlWriter = xmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter, Me)
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                streamReader = New System.IO.StreamReader(memoryStream)
                Return streamReader.ReadToEnd
            Finally
                If (Not (streamReader) Is Nothing) Then
                    streamReader.Dispose()
                End If
                If (Not (memoryStream) Is Nothing) Then
                    memoryStream.Dispose()
                End If
            End Try
        End Function

        Public Overridable Overloads Function Serialize() As String
            Return Serialize(Encoding.UTF8)
        End Function

        '''<summary>
        '''Deserializes workflow markup into an ComprobanteReceptor object
        '''</summary>
        '''<param name="xml">string workflow markup to deserialize</param>
        '''<param name="obj">Output ComprobanteReceptor object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteReceptor, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteReceptor)
            Try
                obj = Deserialize(xml)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteReceptor) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String) As ComprobanteReceptor
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return CType(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), ComprobanteReceptor)
            Finally
                If (Not (stringReader) Is Nothing) Then
                    stringReader.Dispose()
                End If
            End Try
        End Function

        '''<summary>
        '''Serializes current ComprobanteReceptor object into file
        '''</summary>
        '''<param name="fileName">full path of outupt xml file</param>
        '''<param name="exception">output Exception value if failed</param>
        '''<returns>true if can serialize and save into file; otherwise, false</returns>
        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByRef exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding)
            Dim streamWriter As System.IO.StreamWriter = Nothing
            Try
                Dim xmlString As String = Serialize(encoding)
                streamWriter = New System.IO.StreamWriter(fileName, False, encoding.UTF8)
                streamWriter.WriteLine(xmlString)
                streamWriter.Close()
            Finally
                If (Not (streamWriter) Is Nothing) Then
                    streamWriter.Dispose()
                End If
            End Try
        End Sub

        '''<summary>
        '''Deserializes xml markup from file into an ComprobanteReceptor object
        '''</summary>
        '''<param name="fileName">string xml file to load and deserialize</param>
        '''<param name="obj">Output ComprobanteReceptor object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef obj As ComprobanteReceptor, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteReceptor)
            Try
                obj = LoadFromFile(fileName, encoding)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteReceptor, ByRef exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteReceptor) As Boolean
            Dim exception As System.Exception = Nothing
            Return LoadFromFile(fileName, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String) As ComprobanteReceptor
            Return LoadFromFile(fileName, Encoding.UTF8)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As ComprobanteReceptor
            Dim file As System.IO.FileStream = Nothing
            Dim sr As System.IO.StreamReader = Nothing
            Try
                file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
                sr = New System.IO.StreamReader(file, encoding)
                Dim xmlString As String = sr.ReadToEnd
                sr.Close()
                file.Close()
                Return Deserialize(xmlString)
            Finally
                If (Not (file) Is Nothing) Then
                    file.Dispose()
                End If
                If (Not (sr) Is Nothing) Then
                    sr.Dispose()
                End If
            End Try
        End Function
#End Region
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.sat.gob.mx/cfd/3")> _
    Partial Public Class ComprobanteConcepto

        Private itemsField As List(Of Object)

        Private cantidadField As Decimal

        Private unidadField As String

        Private noIdentificacionField As String

        Private descripcionField As String

        Private valorUnitarioField As Decimal

        Private importeField As Decimal

        Private Shared sSerializer As System.Xml.Serialization.XmlSerializer

        Public Sub New()
            MyBase.New()
            Me.itemsField = New List(Of Object)()
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("ComplementoConcepto", GetType(ComprobanteConceptoComplementoConcepto), Order:=0), _
         System.Xml.Serialization.XmlElementAttribute("CuentaPredial", GetType(ComprobanteConceptoCuentaPredial), Order:=0), _
         System.Xml.Serialization.XmlElementAttribute("InformacionAduanera", GetType(t_InformacionAduanera), Order:=0), _
         System.Xml.Serialization.XmlElementAttribute("Parte", GetType(ComprobanteConceptoParte), Order:=0)> _
        Public Property Items() As List(Of Object)
            Get
                Return Me.itemsField
            End Get
            Set(value As List(Of Object))
                Me.itemsField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property cantidad() As Decimal
            Get
                Return Me.cantidadField
            End Get
            Set(value As Decimal)
                Me.cantidadField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property unidad() As String
            Get
                Return Me.unidadField
            End Get
            Set(value As String)
                Me.unidadField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property noIdentificacion() As String
            Get
                Return Me.noIdentificacionField
            End Get
            Set(value As String)
                Me.noIdentificacionField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property descripcion() As String
            Get
                Return Me.descripcionField
            End Get
            Set(value As String)
                Me.descripcionField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property valorUnitario() As Decimal
            Get
                Return Me.valorUnitarioField
            End Get
            Set(value As Decimal)
                Me.valorUnitarioField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property importe() As Decimal
            Get
                Return Me.importeField
            End Get
            Set(value As Decimal)
                Me.importeField = value
            End Set
        End Property

        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (sSerializer Is Nothing) Then
                    sSerializer = New System.Xml.Serialization.XmlSerializer(GetType(ComprobanteConcepto))
                End If
                Return sSerializer
            End Get
        End Property

#Region "Serialize/Deserialize"
        '''<summary>
        '''Serializes current ComprobanteConcepto object into an XML document
        '''</summary>
        '''<returns>string XML value</returns>
        Public Overridable Overloads Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter As System.Xml.XmlWriter = xmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter, Me)
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                streamReader = New System.IO.StreamReader(memoryStream)
                Return streamReader.ReadToEnd
            Finally
                If (Not (streamReader) Is Nothing) Then
                    streamReader.Dispose()
                End If
                If (Not (memoryStream) Is Nothing) Then
                    memoryStream.Dispose()
                End If
            End Try
        End Function

        Public Overridable Overloads Function Serialize() As String
            Return Serialize(Encoding.UTF8)
        End Function

        '''<summary>
        '''Deserializes workflow markup into an ComprobanteConcepto object
        '''</summary>
        '''<param name="xml">string workflow markup to deserialize</param>
        '''<param name="obj">Output ComprobanteConcepto object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteConcepto, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteConcepto)
            Try
                obj = Deserialize(xml)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteConcepto) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String) As ComprobanteConcepto
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return CType(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), ComprobanteConcepto)
            Finally
                If (Not (stringReader) Is Nothing) Then
                    stringReader.Dispose()
                End If
            End Try
        End Function

        '''<summary>
        '''Serializes current ComprobanteConcepto object into file
        '''</summary>
        '''<param name="fileName">full path of outupt xml file</param>
        '''<param name="exception">output Exception value if failed</param>
        '''<returns>true if can serialize and save into file; otherwise, false</returns>
        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByRef exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding)
            Dim streamWriter As System.IO.StreamWriter = Nothing
            Try
                Dim xmlString As String = Serialize(encoding)
                streamWriter = New System.IO.StreamWriter(fileName, False, encoding.UTF8)
                streamWriter.WriteLine(xmlString)
                streamWriter.Close()
            Finally
                If (Not (streamWriter) Is Nothing) Then
                    streamWriter.Dispose()
                End If
            End Try
        End Sub

        '''<summary>
        '''Deserializes xml markup from file into an ComprobanteConcepto object
        '''</summary>
        '''<param name="fileName">string xml file to load and deserialize</param>
        '''<param name="obj">Output ComprobanteConcepto object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef obj As ComprobanteConcepto, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteConcepto)
            Try
                obj = LoadFromFile(fileName, encoding)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteConcepto, ByRef exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteConcepto) As Boolean
            Dim exception As System.Exception = Nothing
            Return LoadFromFile(fileName, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String) As ComprobanteConcepto
            Return LoadFromFile(fileName, Encoding.UTF8)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As ComprobanteConcepto
            Dim file As System.IO.FileStream = Nothing
            Dim sr As System.IO.StreamReader = Nothing
            Try
                file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
                sr = New System.IO.StreamReader(file, encoding)
                Dim xmlString As String = sr.ReadToEnd
                sr.Close()
                file.Close()
                Return Deserialize(xmlString)
            Finally
                If (Not (file) Is Nothing) Then
                    file.Dispose()
                End If
                If (Not (sr) Is Nothing) Then
                    sr.Dispose()
                End If
            End Try
        End Function
#End Region
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.sat.gob.mx/cfd/3")> _
    Partial Public Class ComprobanteConceptoComplementoConcepto

        Private anyField As List(Of System.Xml.XmlElement)

        Private Shared sSerializer As System.Xml.Serialization.XmlSerializer

        Public Sub New()
            MyBase.New()
            Me.anyField = New List(Of System.Xml.XmlElement)()
        End Sub

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=0)> _
        Public Property Any() As List(Of System.Xml.XmlElement)
            Get
                Return Me.anyField
            End Get
            Set(value As List(Of System.Xml.XmlElement))
                Me.anyField = value
            End Set
        End Property

        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (sSerializer Is Nothing) Then
                    sSerializer = New System.Xml.Serialization.XmlSerializer(GetType(ComprobanteConceptoComplementoConcepto))
                End If
                Return sSerializer
            End Get
        End Property

#Region "Serialize/Deserialize"
        '''<summary>
        '''Serializes current ComprobanteConceptoComplementoConcepto object into an XML document
        '''</summary>
        '''<returns>string XML value</returns>
        Public Overridable Overloads Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter As System.Xml.XmlWriter = xmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter, Me)
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                streamReader = New System.IO.StreamReader(memoryStream)
                Return streamReader.ReadToEnd
            Finally
                If (Not (streamReader) Is Nothing) Then
                    streamReader.Dispose()
                End If
                If (Not (memoryStream) Is Nothing) Then
                    memoryStream.Dispose()
                End If
            End Try
        End Function

        Public Overridable Overloads Function Serialize() As String
            Return Serialize(Encoding.UTF8)
        End Function

        '''<summary>
        '''Deserializes workflow markup into an ComprobanteConceptoComplementoConcepto object
        '''</summary>
        '''<param name="xml">string workflow markup to deserialize</param>
        '''<param name="obj">Output ComprobanteConceptoComplementoConcepto object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteConceptoComplementoConcepto, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteConceptoComplementoConcepto)
            Try
                obj = Deserialize(xml)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteConceptoComplementoConcepto) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String) As ComprobanteConceptoComplementoConcepto
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return CType(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), ComprobanteConceptoComplementoConcepto)
            Finally
                If (Not (stringReader) Is Nothing) Then
                    stringReader.Dispose()
                End If
            End Try
        End Function

        '''<summary>
        '''Serializes current ComprobanteConceptoComplementoConcepto object into file
        '''</summary>
        '''<param name="fileName">full path of outupt xml file</param>
        '''<param name="exception">output Exception value if failed</param>
        '''<returns>true if can serialize and save into file; otherwise, false</returns>
        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByRef exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding)
            Dim streamWriter As System.IO.StreamWriter = Nothing
            Try
                Dim xmlString As String = Serialize(encoding)
                streamWriter = New System.IO.StreamWriter(fileName, False, encoding.UTF8)
                streamWriter.WriteLine(xmlString)
                streamWriter.Close()
            Finally
                If (Not (streamWriter) Is Nothing) Then
                    streamWriter.Dispose()
                End If
            End Try
        End Sub

        '''<summary>
        '''Deserializes xml markup from file into an ComprobanteConceptoComplementoConcepto object
        '''</summary>
        '''<param name="fileName">string xml file to load and deserialize</param>
        '''<param name="obj">Output ComprobanteConceptoComplementoConcepto object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef obj As ComprobanteConceptoComplementoConcepto, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteConceptoComplementoConcepto)
            Try
                obj = LoadFromFile(fileName, encoding)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteConceptoComplementoConcepto, ByRef exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteConceptoComplementoConcepto) As Boolean
            Dim exception As System.Exception = Nothing
            Return LoadFromFile(fileName, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String) As ComprobanteConceptoComplementoConcepto
            Return LoadFromFile(fileName, Encoding.UTF8)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As ComprobanteConceptoComplementoConcepto
            Dim file As System.IO.FileStream = Nothing
            Dim sr As System.IO.StreamReader = Nothing
            Try
                file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
                sr = New System.IO.StreamReader(file, encoding)
                Dim xmlString As String = sr.ReadToEnd
                sr.Close()
                file.Close()
                Return Deserialize(xmlString)
            Finally
                If (Not (file) Is Nothing) Then
                    file.Dispose()
                End If
                If (Not (sr) Is Nothing) Then
                    sr.Dispose()
                End If
            End Try
        End Function
#End Region
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.sat.gob.mx/cfd/3")> _
    Partial Public Class ComprobanteConceptoCuentaPredial

        Private numeroField As String

        Private Shared sSerializer As System.Xml.Serialization.XmlSerializer

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property numero() As String
            Get
                Return Me.numeroField
            End Get
            Set(value As String)
                Me.numeroField = value
            End Set
        End Property

        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (sSerializer Is Nothing) Then
                    sSerializer = New System.Xml.Serialization.XmlSerializer(GetType(ComprobanteConceptoCuentaPredial))
                End If
                Return sSerializer
            End Get
        End Property

#Region "Serialize/Deserialize"
        '''<summary>
        '''Serializes current ComprobanteConceptoCuentaPredial object into an XML document
        '''</summary>
        '''<returns>string XML value</returns>
        Public Overridable Overloads Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter As System.Xml.XmlWriter = xmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter, Me)
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                streamReader = New System.IO.StreamReader(memoryStream)
                Return streamReader.ReadToEnd
            Finally
                If (Not (streamReader) Is Nothing) Then
                    streamReader.Dispose()
                End If
                If (Not (memoryStream) Is Nothing) Then
                    memoryStream.Dispose()
                End If
            End Try
        End Function

        Public Overridable Overloads Function Serialize() As String
            Return Serialize(Encoding.UTF8)
        End Function

        '''<summary>
        '''Deserializes workflow markup into an ComprobanteConceptoCuentaPredial object
        '''</summary>
        '''<param name="xml">string workflow markup to deserialize</param>
        '''<param name="obj">Output ComprobanteConceptoCuentaPredial object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteConceptoCuentaPredial, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteConceptoCuentaPredial)
            Try
                obj = Deserialize(xml)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteConceptoCuentaPredial) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String) As ComprobanteConceptoCuentaPredial
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return CType(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), ComprobanteConceptoCuentaPredial)
            Finally
                If (Not (stringReader) Is Nothing) Then
                    stringReader.Dispose()
                End If
            End Try
        End Function

        '''<summary>
        '''Serializes current ComprobanteConceptoCuentaPredial object into file
        '''</summary>
        '''<param name="fileName">full path of outupt xml file</param>
        '''<param name="exception">output Exception value if failed</param>
        '''<returns>true if can serialize and save into file; otherwise, false</returns>
        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByRef exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding)
            Dim streamWriter As System.IO.StreamWriter = Nothing
            Try
                Dim xmlString As String = Serialize(encoding)
                streamWriter = New System.IO.StreamWriter(fileName, False, encoding.UTF8)
                streamWriter.WriteLine(xmlString)
                streamWriter.Close()
            Finally
                If (Not (streamWriter) Is Nothing) Then
                    streamWriter.Dispose()
                End If
            End Try
        End Sub

        '''<summary>
        '''Deserializes xml markup from file into an ComprobanteConceptoCuentaPredial object
        '''</summary>
        '''<param name="fileName">string xml file to load and deserialize</param>
        '''<param name="obj">Output ComprobanteConceptoCuentaPredial object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef obj As ComprobanteConceptoCuentaPredial, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteConceptoCuentaPredial)
            Try
                obj = LoadFromFile(fileName, encoding)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteConceptoCuentaPredial, ByRef exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteConceptoCuentaPredial) As Boolean
            Dim exception As System.Exception = Nothing
            Return LoadFromFile(fileName, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String) As ComprobanteConceptoCuentaPredial
            Return LoadFromFile(fileName, Encoding.UTF8)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As ComprobanteConceptoCuentaPredial
            Dim file As System.IO.FileStream = Nothing
            Dim sr As System.IO.StreamReader = Nothing
            Try
                file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
                sr = New System.IO.StreamReader(file, encoding)
                Dim xmlString As String = sr.ReadToEnd
                sr.Close()
                file.Close()
                Return Deserialize(xmlString)
            Finally
                If (Not (file) Is Nothing) Then
                    file.Dispose()
                End If
                If (Not (sr) Is Nothing) Then
                    sr.Dispose()
                End If
            End Try
        End Function
#End Region
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.sat.gob.mx/cfd/3")> _
    Partial Public Class ComprobanteConceptoParte

        Private informacionAduaneraField As List(Of t_InformacionAduanera)

        Private cantidadField As Decimal

        Private unidadField As String

        Private noIdentificacionField As String

        Private descripcionField As String

        Private valorUnitarioField As Decimal

        Private valorUnitarioFieldSpecified As Boolean

        Private importeField As Decimal

        Private importeFieldSpecified As Boolean

        Private Shared sSerializer As System.Xml.Serialization.XmlSerializer

        Public Sub New()
            MyBase.New()
            Me.informacionAduaneraField = New List(Of t_InformacionAduanera)()
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("InformacionAduanera", Order:=0)> _
        Public Property InformacionAduanera() As List(Of t_InformacionAduanera)
            Get
                Return Me.informacionAduaneraField
            End Get
            Set(value As List(Of t_InformacionAduanera))
                Me.informacionAduaneraField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property cantidad() As Decimal
            Get
                Return Me.cantidadField
            End Get
            Set(value As Decimal)
                Me.cantidadField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property unidad() As String
            Get
                Return Me.unidadField
            End Get
            Set(value As String)
                Me.unidadField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property noIdentificacion() As String
            Get
                Return Me.noIdentificacionField
            End Get
            Set(value As String)
                Me.noIdentificacionField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property descripcion() As String
            Get
                Return Me.descripcionField
            End Get
            Set(value As String)
                Me.descripcionField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property valorUnitario() As Decimal
            Get
                Return Me.valorUnitarioField
            End Get
            Set(value As Decimal)
                Me.valorUnitarioField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property valorUnitarioSpecified() As Boolean
            Get
                Return Me.valorUnitarioFieldSpecified
            End Get
            Set(value As Boolean)
                Me.valorUnitarioFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property importe() As Decimal
            Get
                Return Me.importeField
            End Get
            Set(value As Decimal)
                Me.importeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property importeSpecified() As Boolean
            Get
                Return Me.importeFieldSpecified
            End Get
            Set(value As Boolean)
                Me.importeFieldSpecified = value
            End Set
        End Property

        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (sSerializer Is Nothing) Then
                    sSerializer = New System.Xml.Serialization.XmlSerializer(GetType(ComprobanteConceptoParte))
                End If
                Return sSerializer
            End Get
        End Property

#Region "Serialize/Deserialize"
        '''<summary>
        '''Serializes current ComprobanteConceptoParte object into an XML document
        '''</summary>
        '''<returns>string XML value</returns>
        Public Overridable Overloads Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter As System.Xml.XmlWriter = xmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter, Me)
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                streamReader = New System.IO.StreamReader(memoryStream)
                Return streamReader.ReadToEnd
            Finally
                If (Not (streamReader) Is Nothing) Then
                    streamReader.Dispose()
                End If
                If (Not (memoryStream) Is Nothing) Then
                    memoryStream.Dispose()
                End If
            End Try
        End Function

        Public Overridable Overloads Function Serialize() As String
            Return Serialize(Encoding.UTF8)
        End Function

        '''<summary>
        '''Deserializes workflow markup into an ComprobanteConceptoParte object
        '''</summary>
        '''<param name="xml">string workflow markup to deserialize</param>
        '''<param name="obj">Output ComprobanteConceptoParte object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteConceptoParte, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteConceptoParte)
            Try
                obj = Deserialize(xml)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteConceptoParte) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String) As ComprobanteConceptoParte
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return CType(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), ComprobanteConceptoParte)
            Finally
                If (Not (stringReader) Is Nothing) Then
                    stringReader.Dispose()
                End If
            End Try
        End Function

        '''<summary>
        '''Serializes current ComprobanteConceptoParte object into file
        '''</summary>
        '''<param name="fileName">full path of outupt xml file</param>
        '''<param name="exception">output Exception value if failed</param>
        '''<returns>true if can serialize and save into file; otherwise, false</returns>
        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByRef exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding)
            Dim streamWriter As System.IO.StreamWriter = Nothing
            Try
                Dim xmlString As String = Serialize(encoding)
                streamWriter = New System.IO.StreamWriter(fileName, False, encoding.UTF8)
                streamWriter.WriteLine(xmlString)
                streamWriter.Close()
            Finally
                If (Not (streamWriter) Is Nothing) Then
                    streamWriter.Dispose()
                End If
            End Try
        End Sub

        '''<summary>
        '''Deserializes xml markup from file into an ComprobanteConceptoParte object
        '''</summary>
        '''<param name="fileName">string xml file to load and deserialize</param>
        '''<param name="obj">Output ComprobanteConceptoParte object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef obj As ComprobanteConceptoParte, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteConceptoParte)
            Try
                obj = LoadFromFile(fileName, encoding)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteConceptoParte, ByRef exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteConceptoParte) As Boolean
            Dim exception As System.Exception = Nothing
            Return LoadFromFile(fileName, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String) As ComprobanteConceptoParte
            Return LoadFromFile(fileName, Encoding.UTF8)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As ComprobanteConceptoParte
            Dim file As System.IO.FileStream = Nothing
            Dim sr As System.IO.StreamReader = Nothing
            Try
                file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
                sr = New System.IO.StreamReader(file, encoding)
                Dim xmlString As String = sr.ReadToEnd
                sr.Close()
                file.Close()
                Return Deserialize(xmlString)
            Finally
                If (Not (file) Is Nothing) Then
                    file.Dispose()
                End If
                If (Not (sr) Is Nothing) Then
                    sr.Dispose()
                End If
            End Try
        End Function
#End Region
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.sat.gob.mx/cfd/3")> _
    Partial Public Class ComprobanteImpuestos

        Private retencionesField As List(Of ComprobanteImpuestosRetencion)

        Private trasladosField As List(Of ComprobanteImpuestosTraslado)

        Private totalImpuestosRetenidosField As Decimal

        Private totalImpuestosRetenidosFieldSpecified As Boolean

        Private totalImpuestosTrasladadosField As Decimal

        Private totalImpuestosTrasladadosFieldSpecified As Boolean

        Private Shared sSerializer As System.Xml.Serialization.XmlSerializer

        Public Sub New()
            MyBase.New()
            Me.trasladosField = New List(Of ComprobanteImpuestosTraslado)()
            Me.retencionesField = New List(Of ComprobanteImpuestosRetencion)()
        End Sub

        <System.Xml.Serialization.XmlArrayAttribute(Order:=0), _
         System.Xml.Serialization.XmlArrayItemAttribute("Retencion", IsNullable:=False)> _
        Public Property Retenciones() As List(Of ComprobanteImpuestosRetencion)
            Get
                Return Me.retencionesField
            End Get
            Set(value As List(Of ComprobanteImpuestosRetencion))
                Me.retencionesField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlArrayAttribute(Order:=1), _
         System.Xml.Serialization.XmlArrayItemAttribute("Traslado", IsNullable:=False)> _
        Public Property Traslados() As List(Of ComprobanteImpuestosTraslado)
            Get
                Return Me.trasladosField
            End Get
            Set(value As List(Of ComprobanteImpuestosTraslado))
                Me.trasladosField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property totalImpuestosRetenidos() As Decimal
            Get
                Return Me.totalImpuestosRetenidosField
            End Get
            Set(value As Decimal)
                Me.totalImpuestosRetenidosField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property totalImpuestosRetenidosSpecified() As Boolean
            Get
                Return Me.totalImpuestosRetenidosFieldSpecified
            End Get
            Set(value As Boolean)
                Me.totalImpuestosRetenidosFieldSpecified = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property totalImpuestosTrasladados() As Decimal
            Get
                Return Me.totalImpuestosTrasladadosField
            End Get
            Set(value As Decimal)
                Me.totalImpuestosTrasladadosField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()> _
        Public Property totalImpuestosTrasladadosSpecified() As Boolean
            Get
                Return Me.totalImpuestosTrasladadosFieldSpecified
            End Get
            Set(value As Boolean)
                Me.totalImpuestosTrasladadosFieldSpecified = value
            End Set
        End Property

        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (sSerializer Is Nothing) Then
                    sSerializer = New System.Xml.Serialization.XmlSerializer(GetType(ComprobanteImpuestos))
                End If
                Return sSerializer
            End Get
        End Property

#Region "Serialize/Deserialize"
        '''<summary>
        '''Serializes current ComprobanteImpuestos object into an XML document
        '''</summary>
        '''<returns>string XML value</returns>
        Public Overridable Overloads Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter As System.Xml.XmlWriter = xmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter, Me)
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                streamReader = New System.IO.StreamReader(memoryStream)
                Return streamReader.ReadToEnd
            Finally
                If (Not (streamReader) Is Nothing) Then
                    streamReader.Dispose()
                End If
                If (Not (memoryStream) Is Nothing) Then
                    memoryStream.Dispose()
                End If
            End Try
        End Function

        Public Overridable Overloads Function Serialize() As String
            Return Serialize(Encoding.UTF8)
        End Function

        '''<summary>
        '''Deserializes workflow markup into an ComprobanteImpuestos object
        '''</summary>
        '''<param name="xml">string workflow markup to deserialize</param>
        '''<param name="obj">Output ComprobanteImpuestos object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteImpuestos, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteImpuestos)
            Try
                obj = Deserialize(xml)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteImpuestos) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String) As ComprobanteImpuestos
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return CType(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), ComprobanteImpuestos)
            Finally
                If (Not (stringReader) Is Nothing) Then
                    stringReader.Dispose()
                End If
            End Try
        End Function

        '''<summary>
        '''Serializes current ComprobanteImpuestos object into file
        '''</summary>
        '''<param name="fileName">full path of outupt xml file</param>
        '''<param name="exception">output Exception value if failed</param>
        '''<returns>true if can serialize and save into file; otherwise, false</returns>
        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByRef exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding)
            Dim streamWriter As System.IO.StreamWriter = Nothing
            Try
                Dim xmlString As String = Serialize(encoding)
                streamWriter = New System.IO.StreamWriter(fileName, False, encoding.UTF8)
                streamWriter.WriteLine(xmlString)
                streamWriter.Close()
            Finally
                If (Not (streamWriter) Is Nothing) Then
                    streamWriter.Dispose()
                End If
            End Try
        End Sub

        '''<summary>
        '''Deserializes xml markup from file into an ComprobanteImpuestos object
        '''</summary>
        '''<param name="fileName">string xml file to load and deserialize</param>
        '''<param name="obj">Output ComprobanteImpuestos object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef obj As ComprobanteImpuestos, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteImpuestos)
            Try
                obj = LoadFromFile(fileName, encoding)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteImpuestos, ByRef exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteImpuestos) As Boolean
            Dim exception As System.Exception = Nothing
            Return LoadFromFile(fileName, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String) As ComprobanteImpuestos
            Return LoadFromFile(fileName, Encoding.UTF8)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As ComprobanteImpuestos
            Dim file As System.IO.FileStream = Nothing
            Dim sr As System.IO.StreamReader = Nothing
            Try
                file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
                sr = New System.IO.StreamReader(file, encoding)
                Dim xmlString As String = sr.ReadToEnd
                sr.Close()
                file.Close()
                Return Deserialize(xmlString)
            Finally
                If (Not (file) Is Nothing) Then
                    file.Dispose()
                End If
                If (Not (sr) Is Nothing) Then
                    sr.Dispose()
                End If
            End Try
        End Function
#End Region
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.sat.gob.mx/cfd/3")> _
    Partial Public Class ComprobanteImpuestosRetencion

        Private impuestoField As ComprobanteImpuestosRetencionImpuesto

        Private importeField As Decimal

        Private Shared sSerializer As System.Xml.Serialization.XmlSerializer

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property impuesto() As ComprobanteImpuestosRetencionImpuesto
            Get
                Return Me.impuestoField
            End Get
            Set(value As ComprobanteImpuestosRetencionImpuesto)
                Me.impuestoField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property importe() As Decimal
            Get
                Return Me.importeField
            End Get
            Set(value As Decimal)
                Me.importeField = value
            End Set
        End Property

        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (sSerializer Is Nothing) Then
                    sSerializer = New System.Xml.Serialization.XmlSerializer(GetType(ComprobanteImpuestosRetencion))
                End If
                Return sSerializer
            End Get
        End Property

#Region "Serialize/Deserialize"
        '''<summary>
        '''Serializes current ComprobanteImpuestosRetencion object into an XML document
        '''</summary>
        '''<returns>string XML value</returns>
        Public Overridable Overloads Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter As System.Xml.XmlWriter = xmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter, Me)
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                streamReader = New System.IO.StreamReader(memoryStream)
                Return streamReader.ReadToEnd
            Finally
                If (Not (streamReader) Is Nothing) Then
                    streamReader.Dispose()
                End If
                If (Not (memoryStream) Is Nothing) Then
                    memoryStream.Dispose()
                End If
            End Try
        End Function

        Public Overridable Overloads Function Serialize() As String
            Return Serialize(Encoding.UTF8)
        End Function

        '''<summary>
        '''Deserializes workflow markup into an ComprobanteImpuestosRetencion object
        '''</summary>
        '''<param name="xml">string workflow markup to deserialize</param>
        '''<param name="obj">Output ComprobanteImpuestosRetencion object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteImpuestosRetencion, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteImpuestosRetencion)
            Try
                obj = Deserialize(xml)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteImpuestosRetencion) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String) As ComprobanteImpuestosRetencion
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return CType(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), ComprobanteImpuestosRetencion)
            Finally
                If (Not (stringReader) Is Nothing) Then
                    stringReader.Dispose()
                End If
            End Try
        End Function

        '''<summary>
        '''Serializes current ComprobanteImpuestosRetencion object into file
        '''</summary>
        '''<param name="fileName">full path of outupt xml file</param>
        '''<param name="exception">output Exception value if failed</param>
        '''<returns>true if can serialize and save into file; otherwise, false</returns>
        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByRef exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding)
            Dim streamWriter As System.IO.StreamWriter = Nothing
            Try
                Dim xmlString As String = Serialize(encoding)
                streamWriter = New System.IO.StreamWriter(fileName, False, encoding.UTF8)
                streamWriter.WriteLine(xmlString)
                streamWriter.Close()
            Finally
                If (Not (streamWriter) Is Nothing) Then
                    streamWriter.Dispose()
                End If
            End Try
        End Sub

        '''<summary>
        '''Deserializes xml markup from file into an ComprobanteImpuestosRetencion object
        '''</summary>
        '''<param name="fileName">string xml file to load and deserialize</param>
        '''<param name="obj">Output ComprobanteImpuestosRetencion object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef obj As ComprobanteImpuestosRetencion, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteImpuestosRetencion)
            Try
                obj = LoadFromFile(fileName, encoding)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteImpuestosRetencion, ByRef exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteImpuestosRetencion) As Boolean
            Dim exception As System.Exception = Nothing
            Return LoadFromFile(fileName, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String) As ComprobanteImpuestosRetencion
            Return LoadFromFile(fileName, Encoding.UTF8)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As ComprobanteImpuestosRetencion
            Dim file As System.IO.FileStream = Nothing
            Dim sr As System.IO.StreamReader = Nothing
            Try
                file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
                sr = New System.IO.StreamReader(file, encoding)
                Dim xmlString As String = sr.ReadToEnd
                sr.Close()
                file.Close()
                Return Deserialize(xmlString)
            Finally
                If (Not (file) Is Nothing) Then
                    file.Dispose()
                End If
                If (Not (sr) Is Nothing) Then
                    sr.Dispose()
                End If
            End Try
        End Function
#End Region
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.sat.gob.mx/cfd/3")> _
    Public Enum ComprobanteImpuestosRetencionImpuesto

        '''<remarks/>
        ISR

        '''<remarks/>
        IVA
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.sat.gob.mx/cfd/3")> _
    Partial Public Class ComprobanteImpuestosTraslado

        Private impuestoField As ComprobanteImpuestosTrasladoImpuesto

        Private tasaField As Decimal

        Private importeField As Decimal

        Private Shared sSerializer As System.Xml.Serialization.XmlSerializer

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property impuesto() As ComprobanteImpuestosTrasladoImpuesto
            Get
                Return Me.impuestoField
            End Get
            Set(value As ComprobanteImpuestosTrasladoImpuesto)
                Me.impuestoField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property tasa() As Decimal
            Get
                Return Me.tasaField
            End Get
            Set(value As Decimal)
                Me.tasaField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property importe() As Decimal
            Get
                Return Me.importeField
            End Get
            Set(value As Decimal)
                Me.importeField = value
            End Set
        End Property

        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (sSerializer Is Nothing) Then
                    sSerializer = New System.Xml.Serialization.XmlSerializer(GetType(ComprobanteImpuestosTraslado))
                End If
                Return sSerializer
            End Get
        End Property

#Region "Serialize/Deserialize"
        '''<summary>
        '''Serializes current ComprobanteImpuestosTraslado object into an XML document
        '''</summary>
        '''<returns>string XML value</returns>
        Public Overridable Overloads Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter As System.Xml.XmlWriter = xmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter, Me)
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                streamReader = New System.IO.StreamReader(memoryStream)
                Return streamReader.ReadToEnd
            Finally
                If (Not (streamReader) Is Nothing) Then
                    streamReader.Dispose()
                End If
                If (Not (memoryStream) Is Nothing) Then
                    memoryStream.Dispose()
                End If
            End Try
        End Function

        Public Overridable Overloads Function Serialize() As String
            Return Serialize(Encoding.UTF8)
        End Function

        '''<summary>
        '''Deserializes workflow markup into an ComprobanteImpuestosTraslado object
        '''</summary>
        '''<param name="xml">string workflow markup to deserialize</param>
        '''<param name="obj">Output ComprobanteImpuestosTraslado object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteImpuestosTraslado, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteImpuestosTraslado)
            Try
                obj = Deserialize(xml)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteImpuestosTraslado) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String) As ComprobanteImpuestosTraslado
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return CType(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), ComprobanteImpuestosTraslado)
            Finally
                If (Not (stringReader) Is Nothing) Then
                    stringReader.Dispose()
                End If
            End Try
        End Function

        '''<summary>
        '''Serializes current ComprobanteImpuestosTraslado object into file
        '''</summary>
        '''<param name="fileName">full path of outupt xml file</param>
        '''<param name="exception">output Exception value if failed</param>
        '''<returns>true if can serialize and save into file; otherwise, false</returns>
        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByRef exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding)
            Dim streamWriter As System.IO.StreamWriter = Nothing
            Try
                Dim xmlString As String = Serialize(encoding)
                streamWriter = New System.IO.StreamWriter(fileName, False, encoding.UTF8)
                streamWriter.WriteLine(xmlString)
                streamWriter.Close()
            Finally
                If (Not (streamWriter) Is Nothing) Then
                    streamWriter.Dispose()
                End If
            End Try
        End Sub

        '''<summary>
        '''Deserializes xml markup from file into an ComprobanteImpuestosTraslado object
        '''</summary>
        '''<param name="fileName">string xml file to load and deserialize</param>
        '''<param name="obj">Output ComprobanteImpuestosTraslado object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef obj As ComprobanteImpuestosTraslado, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteImpuestosTraslado)
            Try
                obj = LoadFromFile(fileName, encoding)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteImpuestosTraslado, ByRef exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteImpuestosTraslado) As Boolean
            Dim exception As System.Exception = Nothing
            Return LoadFromFile(fileName, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String) As ComprobanteImpuestosTraslado
            Return LoadFromFile(fileName, Encoding.UTF8)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As ComprobanteImpuestosTraslado
            Dim file As System.IO.FileStream = Nothing
            Dim sr As System.IO.StreamReader = Nothing
            Try
                file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
                sr = New System.IO.StreamReader(file, encoding)
                Dim xmlString As String = sr.ReadToEnd
                sr.Close()
                file.Close()
                Return Deserialize(xmlString)
            Finally
                If (Not (file) Is Nothing) Then
                    file.Dispose()
                End If
                If (Not (sr) Is Nothing) Then
                    sr.Dispose()
                End If
            End Try
        End Function
#End Region
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.sat.gob.mx/cfd/3")> _
    Public Enum ComprobanteImpuestosTrasladoImpuesto

        '''<remarks/>
        IVA

        '''<remarks/>
        IEPS
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.sat.gob.mx/cfd/3")> _
    Partial Public Class ComprobanteComplemento

        Private anyField As List(Of System.Xml.XmlElement)

        Private Shared sSerializer As System.Xml.Serialization.XmlSerializer

        Public Sub New()
            MyBase.New()
            Me.anyField = New List(Of System.Xml.XmlElement)()
        End Sub

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=0)> _
        Public Property Any() As List(Of System.Xml.XmlElement)
            Get
                Return Me.anyField
            End Get
            Set(value As List(Of System.Xml.XmlElement))
                Me.anyField = value
            End Set
        End Property

        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (sSerializer Is Nothing) Then
                    sSerializer = New System.Xml.Serialization.XmlSerializer(GetType(ComprobanteComplemento))
                End If
                Return sSerializer
            End Get
        End Property

#Region "Serialize/Deserialize"
        '''<summary>
        '''Serializes current ComprobanteComplemento object into an XML document
        '''</summary>
        '''<returns>string XML value</returns>
        Public Overridable Overloads Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter As System.Xml.XmlWriter = xmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter, Me)
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                streamReader = New System.IO.StreamReader(memoryStream)
                Return streamReader.ReadToEnd
            Finally
                If (Not (streamReader) Is Nothing) Then
                    streamReader.Dispose()
                End If
                If (Not (memoryStream) Is Nothing) Then
                    memoryStream.Dispose()
                End If
            End Try
        End Function

        Public Overridable Overloads Function Serialize() As String
            Return Serialize(Encoding.UTF8)
        End Function

        '''<summary>
        '''Deserializes workflow markup into an ComprobanteComplemento object
        '''</summary>
        '''<param name="xml">string workflow markup to deserialize</param>
        '''<param name="obj">Output ComprobanteComplemento object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteComplemento, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteComplemento)
            Try
                obj = Deserialize(xml)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteComplemento) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String) As ComprobanteComplemento
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return CType(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), ComprobanteComplemento)
            Finally
                If (Not (stringReader) Is Nothing) Then
                    stringReader.Dispose()
                End If
            End Try
        End Function

        '''<summary>
        '''Serializes current ComprobanteComplemento object into file
        '''</summary>
        '''<param name="fileName">full path of outupt xml file</param>
        '''<param name="exception">output Exception value if failed</param>
        '''<returns>true if can serialize and save into file; otherwise, false</returns>
        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByRef exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding)
            Dim streamWriter As System.IO.StreamWriter = Nothing
            Try
                Dim xmlString As String = Serialize(encoding)
                streamWriter = New System.IO.StreamWriter(fileName, False, encoding.UTF8)
                streamWriter.WriteLine(xmlString)
                streamWriter.Close()
            Finally
                If (Not (streamWriter) Is Nothing) Then
                    streamWriter.Dispose()
                End If
            End Try
        End Sub

        '''<summary>
        '''Deserializes xml markup from file into an ComprobanteComplemento object
        '''</summary>
        '''<param name="fileName">string xml file to load and deserialize</param>
        '''<param name="obj">Output ComprobanteComplemento object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef obj As ComprobanteComplemento, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteComplemento)
            Try
                obj = LoadFromFile(fileName, encoding)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteComplemento, ByRef exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteComplemento) As Boolean
            Dim exception As System.Exception = Nothing
            Return LoadFromFile(fileName, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String) As ComprobanteComplemento
            Return LoadFromFile(fileName, Encoding.UTF8)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As ComprobanteComplemento
            Dim file As System.IO.FileStream = Nothing
            Dim sr As System.IO.StreamReader = Nothing
            Try
                file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
                sr = New System.IO.StreamReader(file, encoding)
                Dim xmlString As String = sr.ReadToEnd
                sr.Close()
                file.Close()
                Return Deserialize(xmlString)
            Finally
                If (Not (file) Is Nothing) Then
                    file.Dispose()
                End If
                If (Not (sr) Is Nothing) Then
                    sr.Dispose()
                End If
            End Try
        End Function
#End Region
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.sat.gob.mx/cfd/3")> _
    Partial Public Class ComprobanteAddenda

        Private anyField As List(Of System.Xml.XmlElement)

        Private Shared sSerializer As System.Xml.Serialization.XmlSerializer

        Public Sub New()
            MyBase.New()
            Me.anyField = New List(Of System.Xml.XmlElement)()
        End Sub

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=0)> _
        Public Property Any() As List(Of System.Xml.XmlElement)
            Get
                Return Me.anyField
            End Get
            Set(value As List(Of System.Xml.XmlElement))
                Me.anyField = value
            End Set
        End Property

        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (sSerializer Is Nothing) Then
                    sSerializer = New System.Xml.Serialization.XmlSerializer(GetType(ComprobanteAddenda))
                End If
                Return sSerializer
            End Get
        End Property

#Region "Serialize/Deserialize"
        '''<summary>
        '''Serializes current ComprobanteAddenda object into an XML document
        '''</summary>
        '''<returns>string XML value</returns>
        Public Overridable Overloads Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As System.Xml.XmlWriterSettings = New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter As System.Xml.XmlWriter = xmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter, Me)
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
                streamReader = New System.IO.StreamReader(memoryStream)
                Return streamReader.ReadToEnd
            Finally
                If (Not (streamReader) Is Nothing) Then
                    streamReader.Dispose()
                End If
                If (Not (memoryStream) Is Nothing) Then
                    memoryStream.Dispose()
                End If
            End Try
        End Function

        Public Overridable Overloads Function Serialize() As String
            Return Serialize(Encoding.UTF8)
        End Function

        '''<summary>
        '''Deserializes workflow markup into an ComprobanteAddenda object
        '''</summary>
        '''<param name="xml">string workflow markup to deserialize</param>
        '''<param name="obj">Output ComprobanteAddenda object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteAddenda, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteAddenda)
            Try
                obj = Deserialize(xml)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String, ByRef obj As ComprobanteAddenda) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Overloads Shared Function Deserialize(ByVal xml As String) As ComprobanteAddenda
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return CType(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), ComprobanteAddenda)
            Finally
                If (Not (stringReader) Is Nothing) Then
                    stringReader.Dispose()
                End If
            End Try
        End Function

        '''<summary>
        '''Serializes current ComprobanteAddenda object into file
        '''</summary>
        '''<param name="fileName">full path of outupt xml file</param>
        '''<param name="exception">output Exception value if failed</param>
        '''<returns>true if can serialize and save into file; otherwise, false</returns>
        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Overloads Function SaveToFile(ByVal fileName As String, ByRef exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Overloads Sub SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding)
            Dim streamWriter As System.IO.StreamWriter = Nothing
            Try
                Dim xmlString As String = Serialize(encoding)
                streamWriter = New System.IO.StreamWriter(fileName, False, encoding.UTF8)
                streamWriter.WriteLine(xmlString)
                streamWriter.Close()
            Finally
                If (Not (streamWriter) Is Nothing) Then
                    streamWriter.Dispose()
                End If
            End Try
        End Sub

        '''<summary>
        '''Deserializes xml markup from file into an ComprobanteAddenda object
        '''</summary>
        '''<param name="fileName">string xml file to load and deserialize</param>
        '''<param name="obj">Output ComprobanteAddenda object</param>
        '''<param name="exception">output Exception value if deserialize failed</param>
        '''<returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByRef obj As ComprobanteAddenda, ByRef exception As System.Exception) As Boolean
            exception = Nothing
            obj = CType(Nothing, ComprobanteAddenda)
            Try
                obj = LoadFromFile(fileName, encoding)
                Return True
            Catch ex As System.Exception
                exception = ex
                Return False
            End Try
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteAddenda, ByRef exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByRef obj As ComprobanteAddenda) As Boolean
            Dim exception As System.Exception = Nothing
            Return LoadFromFile(fileName, obj, exception)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String) As ComprobanteAddenda
            Return LoadFromFile(fileName, Encoding.UTF8)
        End Function

        Public Overloads Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As ComprobanteAddenda
            Dim file As System.IO.FileStream = Nothing
            Dim sr As System.IO.StreamReader = Nothing
            Try
                file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
                sr = New System.IO.StreamReader(file, encoding)
                Dim xmlString As String = sr.ReadToEnd
                sr.Close()
                file.Close()
                Return Deserialize(xmlString)
            Finally
                If (Not (file) Is Nothing) Then
                    file.Dispose()
                End If
                If (Not (sr) Is Nothing) Then
                    sr.Dispose()
                End If
            End Try
        End Function
#End Region
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18034"), _
     System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.sat.gob.mx/cfd/3")> _
    Public Enum ComprobanteTipoDeComprobante

        '''<remarks/>
        ingreso

        '''<remarks/>
        egreso

        '''<remarks/>
        traslado
    End Enum
End Namespace