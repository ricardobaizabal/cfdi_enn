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
Namespace uAddendas.AHM


	<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")> _
	<System.SerializableAttribute> _
	<System.ComponentModel.DesignerCategoryAttribute("code")> _
	<System.Xml.Serialization.XmlTypeAttribute(AnonymousType := True, [Namespace] := "http://www.ahmsa.com/xsd/AddendaAHM1")> _
	<System.Xml.Serialization.XmlRootAttribute([Namespace] := "http://www.ahmsa.com/xsd/AddendaAHM1", IsNullable := False)> _
	Public Partial Class AddendaAHM
		<System.Xml.Serialization.XmlAttribute("schemaLocation", [Namespace] := "http://www.w3.org/2001/XMLSchema-instance")> _
		Public xsiSchemaLocation As String = "http://www.ahmsa.com/xsd/AddendaAHM1 http://www.ahmsa.com/xsd/AddendaAHM1/AddendaAHM.xsd"

		Private documentoField As AddendaAHMDocumento

		Private versionField As String

		Private Shared m_serializer As System.Xml.Serialization.XmlSerializer

		Public Sub New()
			Me.documentoField = New AddendaAHMDocumento()
		End Sub

		<System.Xml.Serialization.XmlElementAttribute(Order := 0)> _
		Public Property Documento() As AddendaAHMDocumento
			Get
				Return Me.documentoField
			End Get
			Set
				Me.documentoField = value
			End Set
		End Property

		<System.Xml.Serialization.XmlAttributeAttribute> _
		Public Property Version() As String
			Get
				Return Me.versionField
			End Get
			Set
				Me.versionField = value
			End Set
		End Property

		Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
			Get
				If (m_serializer Is Nothing) Then
					m_serializer = New System.Xml.Serialization.XmlSerializer(GetType(AddendaAHM))
				End If
				Return m_serializer
			End Get
		End Property

		#Region "Serialize/Deserialize"
		''' <summary>
		''' Serializes current AddendaAHM object into an XML document
		''' </summary>
		''' <returns>string XML value</returns>
		Public Overridable Function Serialize(encoding As System.Text.Encoding) As String
			Dim streamReader As System.IO.StreamReader = Nothing
			Dim memoryStream As System.IO.MemoryStream = Nothing
			Try
				'Name spaces
				Dim xmlNameSpace As New XmlSerializerNamespaces()
				xmlNameSpace.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")
				xmlNameSpace.Add("ahmsa", "http://www.ahmsa.com/xsd/AddendaAHM1")

				memoryStream = New System.IO.MemoryStream()
				Dim xmlWriterSettings As New System.Xml.XmlWriterSettings()
				xmlWriterSettings.Encoding = encoding
				Dim xmlWriter__1 As System.Xml.XmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings)

				'Agregar el xmlNameSpace
				Serializer.Serialize(xmlWriter__1, Me, xmlNameSpace)

				memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
				streamReader = New System.IO.StreamReader(memoryStream)
				Return streamReader.ReadToEnd()
			Finally
				If (streamReader IsNot Nothing) Then
					streamReader.Dispose()
				End If
				If (memoryStream IsNot Nothing) Then
					memoryStream.Dispose()
				End If
			End Try
		End Function

		Public Overridable Function Serialize() As String
			Return Serialize(Encoding.UTF8)
		End Function

		''' <summary>
		''' Deserializes workflow markup into an AddendaAHM object
		''' </summary>
		''' <param name="xml">string workflow markup to deserialize</param>
		''' <param name="obj">Output AddendaAHM object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function Deserialize(xml As String, obj As AddendaAHM, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = Deserialize(xml)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function Deserialize(xml As String, obj As AddendaAHM) As Boolean
			Dim exception As System.Exception = Nothing
			Return Deserialize(xml, obj, exception)
		End Function

		Public Shared Function Deserialize(xml As String) As AddendaAHM
			Dim stringReader As System.IO.StringReader = Nothing
			Try
				stringReader = New System.IO.StringReader(xml)
				Return DirectCast(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), AddendaAHM)
			Finally
				If (stringReader IsNot Nothing) Then
					stringReader.Dispose()
				End If
			End Try
		End Function

		''' <summary>
		''' Serializes current AddendaAHM object into file
		''' </summary>
		''' <param name="fileName">full path of outupt xml file</param>
		''' <param name="exception">output Exception value if failed</param>
		''' <returns>true if can serialize and save into file; otherwise, false</returns>
		Public Overridable Function SaveToFile(fileName As String, encoding As System.Text.Encoding, exception As System.Exception) As Boolean
			exception = Nothing
			Try
				SaveToFile(fileName, encoding)
				Return True
			Catch e As System.Exception
				exception = e
				Return False
			End Try
		End Function

		Public Overridable Function SaveToFile(fileName As String, exception As System.Exception) As Boolean
			Return SaveToFile(fileName, Encoding.UTF8, exception)
		End Function

		Public Overridable Sub SaveToFile(fileName As String)
			SaveToFile(fileName, Encoding.UTF8)
		End Sub

		Public Overridable Sub SaveToFile(fileName As String, encoding__1 As System.Text.Encoding)
			Dim streamWriter As System.IO.StreamWriter = Nothing
			Try
				Dim xmlString As String = Serialize(encoding__1)
				streamWriter = New System.IO.StreamWriter(fileName, False, Encoding.UTF8)
				streamWriter.WriteLine(xmlString)
				streamWriter.Close()
			Finally
				If (streamWriter IsNot Nothing) Then
					streamWriter.Dispose()
				End If
			End Try
		End Sub

		''' <summary>
		''' Deserializes xml markup from file into an AddendaAHM object
		''' </summary>
		''' <param name="fileName">string xml file to load and deserialize</param>
		''' <param name="obj">Output AddendaAHM object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding, obj As AddendaAHM, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = LoadFromFile(fileName, encoding)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHM, exception As System.Exception) As Boolean
			Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHM) As Boolean
			Dim exception As System.Exception = Nothing
			Return LoadFromFile(fileName, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String) As AddendaAHM
			Return LoadFromFile(fileName, Encoding.UTF8)
		End Function

		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding) As AddendaAHM
			Dim file As System.IO.FileStream = Nothing
			Dim sr As System.IO.StreamReader = Nothing
			Try
				file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
				sr = New System.IO.StreamReader(file, encoding)
				Dim xmlString As String = sr.ReadToEnd()
				sr.Close()
				file.Close()
				Return Deserialize(xmlString)
			Finally
				If (file IsNot Nothing) Then
					file.Dispose()
				End If
				If (sr IsNot Nothing) Then
					sr.Dispose()
				End If
			End Try
		End Function
		#End Region
	End Class

	<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")> _
	<System.SerializableAttribute> _
	<System.ComponentModel.DesignerCategoryAttribute("code")> _
	<System.Xml.Serialization.XmlTypeAttribute(AnonymousType := True, [Namespace] := "http://www.ahmsa.com/xsd/AddendaAHM1")> _
	Public Partial Class AddendaAHMDocumento

		Private encabezadoField As AddendaAHMDocumentoEncabezado

		Private detalleField As AddendaAHMDocumentoDetalle

		Private anexosField As List(Of String)

		Private tipoField As String

		Private claseField As String

		Private Shared m_serializer As System.Xml.Serialization.XmlSerializer

		Public Sub New()
			Me.anexosField = New List(Of String)()
			Me.detalleField = New AddendaAHMDocumentoDetalle()
			Me.encabezadoField = New AddendaAHMDocumentoEncabezado()
		End Sub

		<System.Xml.Serialization.XmlElementAttribute(Order := 0)> _
		Public Property Encabezado() As AddendaAHMDocumentoEncabezado
			Get
				Return Me.encabezadoField
			End Get
			Set
				Me.encabezadoField = value
			End Set
		End Property

		<System.Xml.Serialization.XmlElementAttribute(Order := 1)> _
		Public Property Detalle() As AddendaAHMDocumentoDetalle
			Get
				Return Me.detalleField
			End Get
			Set
				Me.detalleField = value
			End Set
		End Property

		<System.Xml.Serialization.XmlArrayAttribute(Order := 2)> _
		<System.Xml.Serialization.XmlArrayItemAttribute("Anexo", IsNullable := False)> _
		Public Property Anexos() As List(Of String)
			Get
				Return Me.anexosField
			End Get
			Set
				Me.anexosField = value
			End Set
		End Property

		<System.Xml.Serialization.XmlAttributeAttribute> _
		Public Property Tipo() As String
			Get
				Return Me.tipoField
			End Get
			Set
				Me.tipoField = value
			End Set
		End Property

		<System.Xml.Serialization.XmlAttributeAttribute> _
		Public Property Clase() As String
			Get
				Return Me.claseField
			End Get
			Set
				Me.claseField = value
			End Set
		End Property

		Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
			Get
				If (m_serializer Is Nothing) Then
					m_serializer = New System.Xml.Serialization.XmlSerializer(GetType(AddendaAHMDocumento))
				End If
				Return m_serializer
			End Get
		End Property

		#Region "Serialize/Deserialize"
		''' <summary>
		''' Serializes current AddendaAHMDocumento object into an XML document
		''' </summary>
		''' <returns>string XML value</returns>
		Public Overridable Function Serialize(encoding As System.Text.Encoding) As String
			Dim streamReader As System.IO.StreamReader = Nothing
			Dim memoryStream As System.IO.MemoryStream = Nothing
			Try
				memoryStream = New System.IO.MemoryStream()
				Dim xmlWriterSettings As New System.Xml.XmlWriterSettings()
				xmlWriterSettings.Encoding = encoding
				Dim xmlWriter__1 As System.Xml.XmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings)
				Serializer.Serialize(xmlWriter__1, Me)
				memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
				streamReader = New System.IO.StreamReader(memoryStream)
				Return streamReader.ReadToEnd()
			Finally
				If (streamReader IsNot Nothing) Then
					streamReader.Dispose()
				End If
				If (memoryStream IsNot Nothing) Then
					memoryStream.Dispose()
				End If
			End Try
		End Function

		Public Overridable Function Serialize() As String
			Return Serialize(Encoding.UTF8)
		End Function

		''' <summary>
		''' Deserializes workflow markup into an AddendaAHMDocumento object
		''' </summary>
		''' <param name="xml">string workflow markup to deserialize</param>
		''' <param name="obj">Output AddendaAHMDocumento object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function Deserialize(xml As String, obj As AddendaAHMDocumento, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = Deserialize(xml)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function Deserialize(xml As String, obj As AddendaAHMDocumento) As Boolean
			Dim exception As System.Exception = Nothing
			Return Deserialize(xml, obj, exception)
		End Function

		Public Shared Function Deserialize(xml As String) As AddendaAHMDocumento
			Dim stringReader As System.IO.StringReader = Nothing
			Try
				stringReader = New System.IO.StringReader(xml)
				Return DirectCast(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), AddendaAHMDocumento)
			Finally
				If (stringReader IsNot Nothing) Then
					stringReader.Dispose()
				End If
			End Try
		End Function

		''' <summary>
		''' Serializes current AddendaAHMDocumento object into file
		''' </summary>
		''' <param name="fileName">full path of outupt xml file</param>
		''' <param name="exception">output Exception value if failed</param>
		''' <returns>true if can serialize and save into file; otherwise, false</returns>
		Public Overridable Function SaveToFile(fileName As String, encoding As System.Text.Encoding, exception As System.Exception) As Boolean
			exception = Nothing
			Try
				SaveToFile(fileName, encoding)
				Return True
			Catch e As System.Exception
				exception = e
				Return False
			End Try
		End Function

		Public Overridable Function SaveToFile(fileName As String, exception As System.Exception) As Boolean
			Return SaveToFile(fileName, Encoding.UTF8, exception)
		End Function

		Public Overridable Sub SaveToFile(fileName As String)
			SaveToFile(fileName, Encoding.UTF8)
		End Sub

		Public Overridable Sub SaveToFile(fileName As String, encoding__1 As System.Text.Encoding)
			Dim streamWriter As System.IO.StreamWriter = Nothing
			Try
				Dim xmlString As String = Serialize(encoding__1)
				streamWriter = New System.IO.StreamWriter(fileName, False, Encoding.UTF8)
				streamWriter.WriteLine(xmlString)
				streamWriter.Close()
			Finally
				If (streamWriter IsNot Nothing) Then
					streamWriter.Dispose()
				End If
			End Try
		End Sub

		''' <summary>
		''' Deserializes xml markup from file into an AddendaAHMDocumento object
		''' </summary>
		''' <param name="fileName">string xml file to load and deserialize</param>
		''' <param name="obj">Output AddendaAHMDocumento object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding, obj As AddendaAHMDocumento, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = LoadFromFile(fileName, encoding)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHMDocumento, exception As System.Exception) As Boolean
			Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHMDocumento) As Boolean
			Dim exception As System.Exception = Nothing
			Return LoadFromFile(fileName, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String) As AddendaAHMDocumento
			Return LoadFromFile(fileName, Encoding.UTF8)
		End Function

		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding) As AddendaAHMDocumento
			Dim file As System.IO.FileStream = Nothing
			Dim sr As System.IO.StreamReader = Nothing
			Try
				file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
				sr = New System.IO.StreamReader(file, encoding)
				Dim xmlString As String = sr.ReadToEnd()
				sr.Close()
				file.Close()
				Return Deserialize(xmlString)
			Finally
				If (file IsNot Nothing) Then
					file.Dispose()
				End If
				If (sr IsNot Nothing) Then
					sr.Dispose()
				End If
			End Try
		End Function
		#End Region
	End Class

	<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")> _
	<System.SerializableAttribute> _
	<System.ComponentModel.DesignerCategoryAttribute("code")> _
	<System.Xml.Serialization.XmlTypeAttribute(AnonymousType := True, [Namespace] := "http://www.ahmsa.com/xsd/AddendaAHM1")> _
	Public Partial Class AddendaAHMDocumentoEncabezado

		Private numSociedadField As String

		Private numDivisionField As String

		Private numProveedorField As String

		Private correoField As String

		Private monedaField As String

		Private Shared m_serializer As System.Xml.Serialization.XmlSerializer

		<System.Xml.Serialization.XmlAttributeAttribute> _
		Public Property NumSociedad() As String
			Get
				Return Me.numSociedadField
			End Get
			Set
				Me.numSociedadField = value
			End Set
		End Property

		<System.Xml.Serialization.XmlAttributeAttribute> _
		Public Property NumDivision() As String
			Get
				Return Me.numDivisionField
			End Get
			Set
				Me.numDivisionField = value
			End Set
		End Property

		<System.Xml.Serialization.XmlAttributeAttribute> _
		Public Property NumProveedor() As String
			Get
				Return Me.numProveedorField
			End Get
			Set
				Me.numProveedorField = value
			End Set
		End Property

		<System.Xml.Serialization.XmlAttributeAttribute> _
		Public Property Correo() As String
			Get
				Return Me.correoField
			End Get
			Set
				Me.correoField = value
			End Set
		End Property

		<System.Xml.Serialization.XmlAttributeAttribute> _
		Public Property Moneda() As String
			Get
				Return Me.monedaField
			End Get
			Set
				Me.monedaField = value
			End Set
		End Property

		Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
			Get
				If (m_serializer Is Nothing) Then
					m_serializer = New System.Xml.Serialization.XmlSerializer(GetType(AddendaAHMDocumentoEncabezado))
				End If
				Return m_serializer
			End Get
		End Property

		#Region "Serialize/Deserialize"
		''' <summary>
		''' Serializes current AddendaAHMDocumentoEncabezado object into an XML document
		''' </summary>
		''' <returns>string XML value</returns>
		Public Overridable Function Serialize(encoding As System.Text.Encoding) As String
			Dim streamReader As System.IO.StreamReader = Nothing
			Dim memoryStream As System.IO.MemoryStream = Nothing
			Try
				memoryStream = New System.IO.MemoryStream()
				Dim xmlWriterSettings As New System.Xml.XmlWriterSettings()
				xmlWriterSettings.Encoding = encoding
				Dim xmlWriter__1 As System.Xml.XmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings)
				Serializer.Serialize(xmlWriter__1, Me)
				memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
				streamReader = New System.IO.StreamReader(memoryStream)
				Return streamReader.ReadToEnd()
			Finally
				If (streamReader IsNot Nothing) Then
					streamReader.Dispose()
				End If
				If (memoryStream IsNot Nothing) Then
					memoryStream.Dispose()
				End If
			End Try
		End Function

		Public Overridable Function Serialize() As String
			Return Serialize(Encoding.UTF8)
		End Function

		''' <summary>
		''' Deserializes workflow markup into an AddendaAHMDocumentoEncabezado object
		''' </summary>
		''' <param name="xml">string workflow markup to deserialize</param>
		''' <param name="obj">Output AddendaAHMDocumentoEncabezado object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function Deserialize(xml As String, obj As AddendaAHMDocumentoEncabezado, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = Deserialize(xml)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function Deserialize(xml As String, obj As AddendaAHMDocumentoEncabezado) As Boolean
			Dim exception As System.Exception = Nothing
			Return Deserialize(xml, obj, exception)
		End Function

		Public Shared Function Deserialize(xml As String) As AddendaAHMDocumentoEncabezado
			Dim stringReader As System.IO.StringReader = Nothing
			Try
				stringReader = New System.IO.StringReader(xml)
				Return DirectCast(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), AddendaAHMDocumentoEncabezado)
			Finally
				If (stringReader IsNot Nothing) Then
					stringReader.Dispose()
				End If
			End Try
		End Function

		''' <summary>
		''' Serializes current AddendaAHMDocumentoEncabezado object into file
		''' </summary>
		''' <param name="fileName">full path of outupt xml file</param>
		''' <param name="exception">output Exception value if failed</param>
		''' <returns>true if can serialize and save into file; otherwise, false</returns>
		Public Overridable Function SaveToFile(fileName As String, encoding As System.Text.Encoding, exception As System.Exception) As Boolean
			exception = Nothing
			Try
				SaveToFile(fileName, encoding)
				Return True
			Catch e As System.Exception
				exception = e
				Return False
			End Try
		End Function

		Public Overridable Function SaveToFile(fileName As String, exception As System.Exception) As Boolean
			Return SaveToFile(fileName, Encoding.UTF8, exception)
		End Function

		Public Overridable Sub SaveToFile(fileName As String)
			SaveToFile(fileName, Encoding.UTF8)
		End Sub

		Public Overridable Sub SaveToFile(fileName As String, encoding__1 As System.Text.Encoding)
			Dim streamWriter As System.IO.StreamWriter = Nothing
			Try
				Dim xmlString As String = Serialize(encoding__1)
				streamWriter = New System.IO.StreamWriter(fileName, False, Encoding.UTF8)
				streamWriter.WriteLine(xmlString)
				streamWriter.Close()
			Finally
				If (streamWriter IsNot Nothing) Then
					streamWriter.Dispose()
				End If
			End Try
		End Sub

		''' <summary>
		''' Deserializes xml markup from file into an AddendaAHMDocumentoEncabezado object
		''' </summary>
		''' <param name="fileName">string xml file to load and deserialize</param>
		''' <param name="obj">Output AddendaAHMDocumentoEncabezado object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding, obj As AddendaAHMDocumentoEncabezado, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = LoadFromFile(fileName, encoding)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHMDocumentoEncabezado, exception As System.Exception) As Boolean
			Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHMDocumentoEncabezado) As Boolean
			Dim exception As System.Exception = Nothing
			Return LoadFromFile(fileName, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String) As AddendaAHMDocumentoEncabezado
			Return LoadFromFile(fileName, Encoding.UTF8)
		End Function

		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding) As AddendaAHMDocumentoEncabezado
			Dim file As System.IO.FileStream = Nothing
			Dim sr As System.IO.StreamReader = Nothing
			Try
				file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
				sr = New System.IO.StreamReader(file, encoding)
				Dim xmlString As String = sr.ReadToEnd()
				sr.Close()
				file.Close()
				Return Deserialize(xmlString)
			Finally
				If (file IsNot Nothing) Then
					file.Dispose()
				End If
				If (sr IsNot Nothing) Then
					sr.Dispose()
				End If
			End Try
		End Function
		#End Region
	End Class

	<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")> _
	<System.SerializableAttribute> _
	<System.ComponentModel.DesignerCategoryAttribute("code")> _
	<System.Xml.Serialization.XmlTypeAttribute(AnonymousType := True, [Namespace] := "http://www.ahmsa.com/xsd/AddendaAHM1")> _
	Public Partial Class AddendaAHMDocumentoDetalle

		Private pedidoField As List(Of AddendaAHMDocumentoDetallePedido)

		Private hojaServicioField As AddendaAHMDocumentoDetalleHojaServicio

		Private transporteField As AddendaAHMDocumentoDetalleTransporte

		Private ctaxPagField As AddendaAHMDocumentoDetalleCtaxPag

		Private liquidacionField As AddendaAHMDocumentoDetalleLiquidacion

		Private Shared m_serializer As System.Xml.Serialization.XmlSerializer

		Public Sub New()
			Me.liquidacionField = New AddendaAHMDocumentoDetalleLiquidacion()
			Me.ctaxPagField = New AddendaAHMDocumentoDetalleCtaxPag()
			Me.transporteField = New AddendaAHMDocumentoDetalleTransporte()
			Me.hojaServicioField = New AddendaAHMDocumentoDetalleHojaServicio()
			Me.pedidoField = New List(Of AddendaAHMDocumentoDetallePedido)()
		End Sub

		<System.Xml.Serialization.XmlElementAttribute("Pedido", Order := 0)> _
		Public Property Pedido() As List(Of AddendaAHMDocumentoDetallePedido)
			Get
				Return Me.pedidoField
			End Get
			Set
				Me.pedidoField = value
			End Set
		End Property

		<System.Xml.Serialization.XmlElementAttribute(Order := 1)> _
		Public Property HojaServicio() As AddendaAHMDocumentoDetalleHojaServicio
			Get
				Return Me.hojaServicioField
			End Get
			Set
				Me.hojaServicioField = value
			End Set
		End Property

		<System.Xml.Serialization.XmlElementAttribute(Order := 2)> _
		Public Property Transporte() As AddendaAHMDocumentoDetalleTransporte
			Get
				Return Me.transporteField
			End Get
			Set
				Me.transporteField = value
			End Set
		End Property

		<System.Xml.Serialization.XmlElementAttribute(Order := 3)> _
		Public Property CtaxPag() As AddendaAHMDocumentoDetalleCtaxPag
			Get
				Return Me.ctaxPagField
			End Get
			Set
				Me.ctaxPagField = value
			End Set
		End Property

		<System.Xml.Serialization.XmlElementAttribute(Order := 4)> _
		Public Property Liquidacion() As AddendaAHMDocumentoDetalleLiquidacion
			Get
				Return Me.liquidacionField
			End Get
			Set
				Me.liquidacionField = value
			End Set
		End Property

		Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
			Get
				If (m_serializer Is Nothing) Then
					m_serializer = New System.Xml.Serialization.XmlSerializer(GetType(AddendaAHMDocumentoDetalle))
				End If
				Return m_serializer
			End Get
		End Property

		#Region "Serialize/Deserialize"
		''' <summary>
		''' Serializes current AddendaAHMDocumentoDetalle object into an XML document
		''' </summary>
		''' <returns>string XML value</returns>
		Public Overridable Function Serialize(encoding As System.Text.Encoding) As String
			Dim streamReader As System.IO.StreamReader = Nothing
			Dim memoryStream As System.IO.MemoryStream = Nothing
			Try
				memoryStream = New System.IO.MemoryStream()
				Dim xmlWriterSettings As New System.Xml.XmlWriterSettings()
				xmlWriterSettings.Encoding = encoding
				Dim xmlWriter__1 As System.Xml.XmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings)
				Serializer.Serialize(xmlWriter__1, Me)
				memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
				streamReader = New System.IO.StreamReader(memoryStream)
				Return streamReader.ReadToEnd()
			Finally
				If (streamReader IsNot Nothing) Then
					streamReader.Dispose()
				End If
				If (memoryStream IsNot Nothing) Then
					memoryStream.Dispose()
				End If
			End Try
		End Function

		Public Overridable Function Serialize() As String
			Return Serialize(Encoding.UTF8)
		End Function

		''' <summary>
		''' Deserializes workflow markup into an AddendaAHMDocumentoDetalle object
		''' </summary>
		''' <param name="xml">string workflow markup to deserialize</param>
		''' <param name="obj">Output AddendaAHMDocumentoDetalle object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function Deserialize(xml As String, obj As AddendaAHMDocumentoDetalle, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = Deserialize(xml)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function Deserialize(xml As String, obj As AddendaAHMDocumentoDetalle) As Boolean
			Dim exception As System.Exception = Nothing
			Return Deserialize(xml, obj, exception)
		End Function

		Public Shared Function Deserialize(xml As String) As AddendaAHMDocumentoDetalle
			Dim stringReader As System.IO.StringReader = Nothing
			Try
				stringReader = New System.IO.StringReader(xml)
				Return DirectCast(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), AddendaAHMDocumentoDetalle)
			Finally
				If (stringReader IsNot Nothing) Then
					stringReader.Dispose()
				End If
			End Try
		End Function

		''' <summary>
		''' Serializes current AddendaAHMDocumentoDetalle object into file
		''' </summary>
		''' <param name="fileName">full path of outupt xml file</param>
		''' <param name="exception">output Exception value if failed</param>
		''' <returns>true if can serialize and save into file; otherwise, false</returns>
		Public Overridable Function SaveToFile(fileName As String, encoding As System.Text.Encoding, exception As System.Exception) As Boolean
			exception = Nothing
			Try
				SaveToFile(fileName, encoding)
				Return True
			Catch e As System.Exception
				exception = e
				Return False
			End Try
		End Function

		Public Overridable Function SaveToFile(fileName As String, exception As System.Exception) As Boolean
			Return SaveToFile(fileName, Encoding.UTF8, exception)
		End Function

		Public Overridable Sub SaveToFile(fileName As String)
			SaveToFile(fileName, Encoding.UTF8)
		End Sub

		Public Overridable Sub SaveToFile(fileName As String, encoding__1 As System.Text.Encoding)
			Dim streamWriter As System.IO.StreamWriter = Nothing
			Try
				Dim xmlString As String = Serialize(encoding__1)
				streamWriter = New System.IO.StreamWriter(fileName, False, Encoding.UTF8)
				streamWriter.WriteLine(xmlString)
				streamWriter.Close()
			Finally
				If (streamWriter IsNot Nothing) Then
					streamWriter.Dispose()
				End If
			End Try
		End Sub

		''' <summary>
		''' Deserializes xml markup from file into an AddendaAHMDocumentoDetalle object
		''' </summary>
		''' <param name="fileName">string xml file to load and deserialize</param>
		''' <param name="obj">Output AddendaAHMDocumentoDetalle object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding, obj As AddendaAHMDocumentoDetalle, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = LoadFromFile(fileName, encoding)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHMDocumentoDetalle, exception As System.Exception) As Boolean
			Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHMDocumentoDetalle) As Boolean
			Dim exception As System.Exception = Nothing
			Return LoadFromFile(fileName, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String) As AddendaAHMDocumentoDetalle
			Return LoadFromFile(fileName, Encoding.UTF8)
		End Function

		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding) As AddendaAHMDocumentoDetalle
			Dim file As System.IO.FileStream = Nothing
			Dim sr As System.IO.StreamReader = Nothing
			Try
				file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
				sr = New System.IO.StreamReader(file, encoding)
				Dim xmlString As String = sr.ReadToEnd()
				sr.Close()
				file.Close()
				Return Deserialize(xmlString)
			Finally
				If (file IsNot Nothing) Then
					file.Dispose()
				End If
				If (sr IsNot Nothing) Then
					sr.Dispose()
				End If
			End Try
		End Function
		#End Region
	End Class

	<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")> _
	<System.SerializableAttribute> _
	<System.ComponentModel.DesignerCategoryAttribute("code")> _
	<System.Xml.Serialization.XmlTypeAttribute(AnonymousType := True, [Namespace] := "http://www.ahmsa.com/xsd/AddendaAHM1")> _
	Public Partial Class AddendaAHMDocumentoDetallePedido

		Private recepcionField As List(Of Object)

		Private numField As String

		Private Shared m_serializer As System.Xml.Serialization.XmlSerializer

		Public Sub New()
			Me.recepcionField = New List(Of Object)()
		End Sub

		<System.Xml.Serialization.XmlElementAttribute("Recepcion", IsNullable := True, Order := 0)> _
		Public Property Recepcion() As List(Of Object)
			Get
				Return Me.recepcionField
			End Get
			Set
				Me.recepcionField = value
			End Set
		End Property

		<System.Xml.Serialization.XmlAttributeAttribute> _
		Public Property Num() As String
			Get
				Return Me.numField
			End Get
			Set
				Me.numField = value
			End Set
		End Property

		Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
			Get
				If (m_serializer Is Nothing) Then
					m_serializer = New System.Xml.Serialization.XmlSerializer(GetType(AddendaAHMDocumentoDetallePedido))
				End If
				Return m_serializer
			End Get
		End Property

		#Region "Serialize/Deserialize"
		''' <summary>
		''' Serializes current AddendaAHMDocumentoDetallePedido object into an XML document
		''' </summary>
		''' <returns>string XML value</returns>
		Public Overridable Function Serialize(encoding As System.Text.Encoding) As String
			Dim streamReader As System.IO.StreamReader = Nothing
			Dim memoryStream As System.IO.MemoryStream = Nothing
			Try
				memoryStream = New System.IO.MemoryStream()
				Dim xmlWriterSettings As New System.Xml.XmlWriterSettings()
				xmlWriterSettings.Encoding = encoding
				Dim xmlWriter__1 As System.Xml.XmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings)
				Serializer.Serialize(xmlWriter__1, Me)
				memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
				streamReader = New System.IO.StreamReader(memoryStream)
				Return streamReader.ReadToEnd()
			Finally
				If (streamReader IsNot Nothing) Then
					streamReader.Dispose()
				End If
				If (memoryStream IsNot Nothing) Then
					memoryStream.Dispose()
				End If
			End Try
		End Function

		Public Overridable Function Serialize() As String
			Return Serialize(Encoding.UTF8)
		End Function

		''' <summary>
		''' Deserializes workflow markup into an AddendaAHMDocumentoDetallePedido object
		''' </summary>
		''' <param name="xml">string workflow markup to deserialize</param>
		''' <param name="obj">Output AddendaAHMDocumentoDetallePedido object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function Deserialize(xml As String, obj As AddendaAHMDocumentoDetallePedido, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = Deserialize(xml)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function Deserialize(xml As String, obj As AddendaAHMDocumentoDetallePedido) As Boolean
			Dim exception As System.Exception = Nothing
			Return Deserialize(xml, obj, exception)
		End Function

		Public Shared Function Deserialize(xml As String) As AddendaAHMDocumentoDetallePedido
			Dim stringReader As System.IO.StringReader = Nothing
			Try
				stringReader = New System.IO.StringReader(xml)
				Return DirectCast(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), AddendaAHMDocumentoDetallePedido)
			Finally
				If (stringReader IsNot Nothing) Then
					stringReader.Dispose()
				End If
			End Try
		End Function

		''' <summary>
		''' Serializes current AddendaAHMDocumentoDetallePedido object into file
		''' </summary>
		''' <param name="fileName">full path of outupt xml file</param>
		''' <param name="exception">output Exception value if failed</param>
		''' <returns>true if can serialize and save into file; otherwise, false</returns>
		Public Overridable Function SaveToFile(fileName As String, encoding As System.Text.Encoding, exception As System.Exception) As Boolean
			exception = Nothing
			Try
				SaveToFile(fileName, encoding)
				Return True
			Catch e As System.Exception
				exception = e
				Return False
			End Try
		End Function

		Public Overridable Function SaveToFile(fileName As String, exception As System.Exception) As Boolean
			Return SaveToFile(fileName, Encoding.UTF8, exception)
		End Function

		Public Overridable Sub SaveToFile(fileName As String)
			SaveToFile(fileName, Encoding.UTF8)
		End Sub

		Public Overridable Sub SaveToFile(fileName As String, encoding__1 As System.Text.Encoding)
			Dim streamWriter As System.IO.StreamWriter = Nothing
			Try
				Dim xmlString As String = Serialize(encoding__1)
				streamWriter = New System.IO.StreamWriter(fileName, False, Encoding.UTF8)
				streamWriter.WriteLine(xmlString)
				streamWriter.Close()
			Finally
				If (streamWriter IsNot Nothing) Then
					streamWriter.Dispose()
				End If
			End Try
		End Sub

		''' <summary>
		''' Deserializes xml markup from file into an AddendaAHMDocumentoDetallePedido object
		''' </summary>
		''' <param name="fileName">string xml file to load and deserialize</param>
		''' <param name="obj">Output AddendaAHMDocumentoDetallePedido object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding, obj As AddendaAHMDocumentoDetallePedido, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = LoadFromFile(fileName, encoding)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHMDocumentoDetallePedido, exception As System.Exception) As Boolean
			Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHMDocumentoDetallePedido) As Boolean
			Dim exception As System.Exception = Nothing
			Return LoadFromFile(fileName, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String) As AddendaAHMDocumentoDetallePedido
			Return LoadFromFile(fileName, Encoding.UTF8)
		End Function

		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding) As AddendaAHMDocumentoDetallePedido
			Dim file As System.IO.FileStream = Nothing
			Dim sr As System.IO.StreamReader = Nothing
			Try
				file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
				sr = New System.IO.StreamReader(file, encoding)
				Dim xmlString As String = sr.ReadToEnd()
				sr.Close()
				file.Close()
				Return Deserialize(xmlString)
			Finally
				If (file IsNot Nothing) Then
					file.Dispose()
				End If
				If (sr IsNot Nothing) Then
					sr.Dispose()
				End If
			End Try
		End Function
		#End Region
	End Class

	<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")> _
	<System.SerializableAttribute> _
	<System.ComponentModel.DesignerCategoryAttribute("code")> _
	<System.Xml.Serialization.XmlTypeAttribute(AnonymousType := True, [Namespace] := "http://www.ahmsa.com/xsd/AddendaAHM1")> _
	Public Partial Class AddendaAHMDocumentoDetalleHojaServicio

		Private numField As String

		Private Shared m_serializer As System.Xml.Serialization.XmlSerializer

		<System.Xml.Serialization.XmlAttributeAttribute> _
		Public Property Num() As String
			Get
				Return Me.numField
			End Get
			Set
				Me.numField = value
			End Set
		End Property

		Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
			Get
				If (m_serializer Is Nothing) Then
					m_serializer = New System.Xml.Serialization.XmlSerializer(GetType(AddendaAHMDocumentoDetalleHojaServicio))
				End If
				Return m_serializer
			End Get
		End Property

		#Region "Serialize/Deserialize"
		''' <summary>
		''' Serializes current AddendaAHMDocumentoDetalleHojaServicio object into an XML document
		''' </summary>
		''' <returns>string XML value</returns>
		Public Overridable Function Serialize(encoding As System.Text.Encoding) As String
			Dim streamReader As System.IO.StreamReader = Nothing
			Dim memoryStream As System.IO.MemoryStream = Nothing
			Try
				memoryStream = New System.IO.MemoryStream()
				Dim xmlWriterSettings As New System.Xml.XmlWriterSettings()
				xmlWriterSettings.Encoding = encoding
				Dim xmlWriter__1 As System.Xml.XmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings)
				Serializer.Serialize(xmlWriter__1, Me)
				memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
				streamReader = New System.IO.StreamReader(memoryStream)
				Return streamReader.ReadToEnd()
			Finally
				If (streamReader IsNot Nothing) Then
					streamReader.Dispose()
				End If
				If (memoryStream IsNot Nothing) Then
					memoryStream.Dispose()
				End If
			End Try
		End Function

		Public Overridable Function Serialize() As String
			Return Serialize(Encoding.UTF8)
		End Function

		''' <summary>
		''' Deserializes workflow markup into an AddendaAHMDocumentoDetalleHojaServicio object
		''' </summary>
		''' <param name="xml">string workflow markup to deserialize</param>
		''' <param name="obj">Output AddendaAHMDocumentoDetalleHojaServicio object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function Deserialize(xml As String, obj As AddendaAHMDocumentoDetalleHojaServicio, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = Deserialize(xml)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function Deserialize(xml As String, obj As AddendaAHMDocumentoDetalleHojaServicio) As Boolean
			Dim exception As System.Exception = Nothing
			Return Deserialize(xml, obj, exception)
		End Function

		Public Shared Function Deserialize(xml As String) As AddendaAHMDocumentoDetalleHojaServicio
			Dim stringReader As System.IO.StringReader = Nothing
			Try
				stringReader = New System.IO.StringReader(xml)
				Return DirectCast(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), AddendaAHMDocumentoDetalleHojaServicio)
			Finally
				If (stringReader IsNot Nothing) Then
					stringReader.Dispose()
				End If
			End Try
		End Function

		''' <summary>
		''' Serializes current AddendaAHMDocumentoDetalleHojaServicio object into file
		''' </summary>
		''' <param name="fileName">full path of outupt xml file</param>
		''' <param name="exception">output Exception value if failed</param>
		''' <returns>true if can serialize and save into file; otherwise, false</returns>
		Public Overridable Function SaveToFile(fileName As String, encoding As System.Text.Encoding, exception As System.Exception) As Boolean
			exception = Nothing
			Try
				SaveToFile(fileName, encoding)
				Return True
			Catch e As System.Exception
				exception = e
				Return False
			End Try
		End Function

		Public Overridable Function SaveToFile(fileName As String, exception As System.Exception) As Boolean
			Return SaveToFile(fileName, Encoding.UTF8, exception)
		End Function

		Public Overridable Sub SaveToFile(fileName As String)
			SaveToFile(fileName, Encoding.UTF8)
		End Sub

		Public Overridable Sub SaveToFile(fileName As String, encoding__1 As System.Text.Encoding)
			Dim streamWriter As System.IO.StreamWriter = Nothing
			Try
				Dim xmlString As String = Serialize(encoding__1)
				streamWriter = New System.IO.StreamWriter(fileName, False, Encoding.UTF8)
				streamWriter.WriteLine(xmlString)
				streamWriter.Close()
			Finally
				If (streamWriter IsNot Nothing) Then
					streamWriter.Dispose()
				End If
			End Try
		End Sub

		''' <summary>
		''' Deserializes xml markup from file into an AddendaAHMDocumentoDetalleHojaServicio object
		''' </summary>
		''' <param name="fileName">string xml file to load and deserialize</param>
		''' <param name="obj">Output AddendaAHMDocumentoDetalleHojaServicio object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding, obj As AddendaAHMDocumentoDetalleHojaServicio, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = LoadFromFile(fileName, encoding)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHMDocumentoDetalleHojaServicio, exception As System.Exception) As Boolean
			Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHMDocumentoDetalleHojaServicio) As Boolean
			Dim exception As System.Exception = Nothing
			Return LoadFromFile(fileName, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String) As AddendaAHMDocumentoDetalleHojaServicio
			Return LoadFromFile(fileName, Encoding.UTF8)
		End Function

		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding) As AddendaAHMDocumentoDetalleHojaServicio
			Dim file As System.IO.FileStream = Nothing
			Dim sr As System.IO.StreamReader = Nothing
			Try
				file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
				sr = New System.IO.StreamReader(file, encoding)
				Dim xmlString As String = sr.ReadToEnd()
				sr.Close()
				file.Close()
				Return Deserialize(xmlString)
			Finally
				If (file IsNot Nothing) Then
					file.Dispose()
				End If
				If (sr IsNot Nothing) Then
					sr.Dispose()
				End If
			End Try
		End Function
		#End Region
	End Class

	<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")> _
	<System.SerializableAttribute> _
	<System.ComponentModel.DesignerCategoryAttribute("code")> _
	<System.Xml.Serialization.XmlTypeAttribute(AnonymousType := True, [Namespace] := "http://www.ahmsa.com/xsd/AddendaAHM1")> _
	Public Partial Class AddendaAHMDocumentoDetalleTransporte

		Private numField As String

		Private Shared m_serializer As System.Xml.Serialization.XmlSerializer

		<System.Xml.Serialization.XmlAttributeAttribute> _
		Public Property Num() As String
			Get
				Return Me.numField
			End Get
			Set
				Me.numField = value
			End Set
		End Property

		Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
			Get
				If (m_serializer Is Nothing) Then
					m_serializer = New System.Xml.Serialization.XmlSerializer(GetType(AddendaAHMDocumentoDetalleTransporte))
				End If
				Return m_serializer
			End Get
		End Property

		#Region "Serialize/Deserialize"
		''' <summary>
		''' Serializes current AddendaAHMDocumentoDetalleTransporte object into an XML document
		''' </summary>
		''' <returns>string XML value</returns>
		Public Overridable Function Serialize(encoding As System.Text.Encoding) As String
			Dim streamReader As System.IO.StreamReader = Nothing
			Dim memoryStream As System.IO.MemoryStream = Nothing
			Try
				memoryStream = New System.IO.MemoryStream()
				Dim xmlWriterSettings As New System.Xml.XmlWriterSettings()
				xmlWriterSettings.Encoding = encoding
				Dim xmlWriter__1 As System.Xml.XmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings)
				Serializer.Serialize(xmlWriter__1, Me)
				memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
				streamReader = New System.IO.StreamReader(memoryStream)
				Return streamReader.ReadToEnd()
			Finally
				If (streamReader IsNot Nothing) Then
					streamReader.Dispose()
				End If
				If (memoryStream IsNot Nothing) Then
					memoryStream.Dispose()
				End If
			End Try
		End Function

		Public Overridable Function Serialize() As String
			Return Serialize(Encoding.UTF8)
		End Function

		''' <summary>
		''' Deserializes workflow markup into an AddendaAHMDocumentoDetalleTransporte object
		''' </summary>
		''' <param name="xml">string workflow markup to deserialize</param>
		''' <param name="obj">Output AddendaAHMDocumentoDetalleTransporte object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function Deserialize(xml As String, obj As AddendaAHMDocumentoDetalleTransporte, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = Deserialize(xml)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function Deserialize(xml As String, obj As AddendaAHMDocumentoDetalleTransporte) As Boolean
			Dim exception As System.Exception = Nothing
			Return Deserialize(xml, obj, exception)
		End Function

		Public Shared Function Deserialize(xml As String) As AddendaAHMDocumentoDetalleTransporte
			Dim stringReader As System.IO.StringReader = Nothing
			Try
				stringReader = New System.IO.StringReader(xml)
				Return DirectCast(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), AddendaAHMDocumentoDetalleTransporte)
			Finally
				If (stringReader IsNot Nothing) Then
					stringReader.Dispose()
				End If
			End Try
		End Function

		''' <summary>
		''' Serializes current AddendaAHMDocumentoDetalleTransporte object into file
		''' </summary>
		''' <param name="fileName">full path of outupt xml file</param>
		''' <param name="exception">output Exception value if failed</param>
		''' <returns>true if can serialize and save into file; otherwise, false</returns>
		Public Overridable Function SaveToFile(fileName As String, encoding As System.Text.Encoding, exception As System.Exception) As Boolean
			exception = Nothing
			Try
				SaveToFile(fileName, encoding)
				Return True
			Catch e As System.Exception
				exception = e
				Return False
			End Try
		End Function

		Public Overridable Function SaveToFile(fileName As String, exception As System.Exception) As Boolean
			Return SaveToFile(fileName, Encoding.UTF8, exception)
		End Function

		Public Overridable Sub SaveToFile(fileName As String)
			SaveToFile(fileName, Encoding.UTF8)
		End Sub

		Public Overridable Sub SaveToFile(fileName As String, encoding__1 As System.Text.Encoding)
			Dim streamWriter As System.IO.StreamWriter = Nothing
			Try
				Dim xmlString As String = Serialize(encoding__1)
				streamWriter = New System.IO.StreamWriter(fileName, False, Encoding.UTF8)
				streamWriter.WriteLine(xmlString)
				streamWriter.Close()
			Finally
				If (streamWriter IsNot Nothing) Then
					streamWriter.Dispose()
				End If
			End Try
		End Sub

		''' <summary>
		''' Deserializes xml markup from file into an AddendaAHMDocumentoDetalleTransporte object
		''' </summary>
		''' <param name="fileName">string xml file to load and deserialize</param>
		''' <param name="obj">Output AddendaAHMDocumentoDetalleTransporte object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding, obj As AddendaAHMDocumentoDetalleTransporte, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = LoadFromFile(fileName, encoding)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHMDocumentoDetalleTransporte, exception As System.Exception) As Boolean
			Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHMDocumentoDetalleTransporte) As Boolean
			Dim exception As System.Exception = Nothing
			Return LoadFromFile(fileName, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String) As AddendaAHMDocumentoDetalleTransporte
			Return LoadFromFile(fileName, Encoding.UTF8)
		End Function

		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding) As AddendaAHMDocumentoDetalleTransporte
			Dim file As System.IO.FileStream = Nothing
			Dim sr As System.IO.StreamReader = Nothing
			Try
				file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
				sr = New System.IO.StreamReader(file, encoding)
				Dim xmlString As String = sr.ReadToEnd()
				sr.Close()
				file.Close()
				Return Deserialize(xmlString)
			Finally
				If (file IsNot Nothing) Then
					file.Dispose()
				End If
				If (sr IsNot Nothing) Then
					sr.Dispose()
				End If
			End Try
		End Function
		#End Region
	End Class

	<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")> _
	<System.SerializableAttribute> _
	<System.ComponentModel.DesignerCategoryAttribute("code")> _
	<System.Xml.Serialization.XmlTypeAttribute(AnonymousType := True, [Namespace] := "http://www.ahmsa.com/xsd/AddendaAHM1")> _
	Public Partial Class AddendaAHMDocumentoDetalleCtaxPag

		Private numField As String

		Private ejercicioField As String

		Private Shared m_serializer As System.Xml.Serialization.XmlSerializer

		<System.Xml.Serialization.XmlAttributeAttribute> _
		Public Property Num() As String
			Get
				Return Me.numField
			End Get
			Set
				Me.numField = value
			End Set
		End Property

		<System.Xml.Serialization.XmlAttributeAttribute> _
		Public Property Ejercicio() As String
			Get
				Return Me.ejercicioField
			End Get
			Set
				Me.ejercicioField = value
			End Set
		End Property

		Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
			Get
				If (m_serializer Is Nothing) Then
					m_serializer = New System.Xml.Serialization.XmlSerializer(GetType(AddendaAHMDocumentoDetalleCtaxPag))
				End If
				Return m_serializer
			End Get
		End Property

		#Region "Serialize/Deserialize"
		''' <summary>
		''' Serializes current AddendaAHMDocumentoDetalleCtaxPag object into an XML document
		''' </summary>
		''' <returns>string XML value</returns>
		Public Overridable Function Serialize(encoding As System.Text.Encoding) As String
			Dim streamReader As System.IO.StreamReader = Nothing
			Dim memoryStream As System.IO.MemoryStream = Nothing
			Try
				memoryStream = New System.IO.MemoryStream()
				Dim xmlWriterSettings As New System.Xml.XmlWriterSettings()
				xmlWriterSettings.Encoding = encoding
				Dim xmlWriter__1 As System.Xml.XmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings)
				Serializer.Serialize(xmlWriter__1, Me)
				memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
				streamReader = New System.IO.StreamReader(memoryStream)
				Return streamReader.ReadToEnd()
			Finally
				If (streamReader IsNot Nothing) Then
					streamReader.Dispose()
				End If
				If (memoryStream IsNot Nothing) Then
					memoryStream.Dispose()
				End If
			End Try
		End Function

		Public Overridable Function Serialize() As String
			Return Serialize(Encoding.UTF8)
		End Function

		''' <summary>
		''' Deserializes workflow markup into an AddendaAHMDocumentoDetalleCtaxPag object
		''' </summary>
		''' <param name="xml">string workflow markup to deserialize</param>
		''' <param name="obj">Output AddendaAHMDocumentoDetalleCtaxPag object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function Deserialize(xml As String, obj As AddendaAHMDocumentoDetalleCtaxPag, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = Deserialize(xml)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function Deserialize(xml As String, obj As AddendaAHMDocumentoDetalleCtaxPag) As Boolean
			Dim exception As System.Exception = Nothing
			Return Deserialize(xml, obj, exception)
		End Function

		Public Shared Function Deserialize(xml As String) As AddendaAHMDocumentoDetalleCtaxPag
			Dim stringReader As System.IO.StringReader = Nothing
			Try
				stringReader = New System.IO.StringReader(xml)
				Return DirectCast(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), AddendaAHMDocumentoDetalleCtaxPag)
			Finally
				If (stringReader IsNot Nothing) Then
					stringReader.Dispose()
				End If
			End Try
		End Function

		''' <summary>
		''' Serializes current AddendaAHMDocumentoDetalleCtaxPag object into file
		''' </summary>
		''' <param name="fileName">full path of outupt xml file</param>
		''' <param name="exception">output Exception value if failed</param>
		''' <returns>true if can serialize and save into file; otherwise, false</returns>
		Public Overridable Function SaveToFile(fileName As String, encoding As System.Text.Encoding, exception As System.Exception) As Boolean
			exception = Nothing
			Try
				SaveToFile(fileName, encoding)
				Return True
			Catch e As System.Exception
				exception = e
				Return False
			End Try
		End Function

		Public Overridable Function SaveToFile(fileName As String, exception As System.Exception) As Boolean
			Return SaveToFile(fileName, Encoding.UTF8, exception)
		End Function

		Public Overridable Sub SaveToFile(fileName As String)
			SaveToFile(fileName, Encoding.UTF8)
		End Sub

		Public Overridable Sub SaveToFile(fileName As String, encoding__1 As System.Text.Encoding)
			Dim streamWriter As System.IO.StreamWriter = Nothing
			Try
				Dim xmlString As String = Serialize(encoding__1)
				streamWriter = New System.IO.StreamWriter(fileName, False, Encoding.UTF8)
				streamWriter.WriteLine(xmlString)
				streamWriter.Close()
			Finally
				If (streamWriter IsNot Nothing) Then
					streamWriter.Dispose()
				End If
			End Try
		End Sub

		''' <summary>
		''' Deserializes xml markup from file into an AddendaAHMDocumentoDetalleCtaxPag object
		''' </summary>
		''' <param name="fileName">string xml file to load and deserialize</param>
		''' <param name="obj">Output AddendaAHMDocumentoDetalleCtaxPag object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding, obj As AddendaAHMDocumentoDetalleCtaxPag, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = LoadFromFile(fileName, encoding)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHMDocumentoDetalleCtaxPag, exception As System.Exception) As Boolean
			Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHMDocumentoDetalleCtaxPag) As Boolean
			Dim exception As System.Exception = Nothing
			Return LoadFromFile(fileName, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String) As AddendaAHMDocumentoDetalleCtaxPag
			Return LoadFromFile(fileName, Encoding.UTF8)
		End Function

		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding) As AddendaAHMDocumentoDetalleCtaxPag
			Dim file As System.IO.FileStream = Nothing
			Dim sr As System.IO.StreamReader = Nothing
			Try
				file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
				sr = New System.IO.StreamReader(file, encoding)
				Dim xmlString As String = sr.ReadToEnd()
				sr.Close()
				file.Close()
				Return Deserialize(xmlString)
			Finally
				If (file IsNot Nothing) Then
					file.Dispose()
				End If
				If (sr IsNot Nothing) Then
					sr.Dispose()
				End If
			End Try
		End Function
		#End Region
	End Class

	<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")> _
	<System.SerializableAttribute> _
	<System.ComponentModel.DesignerCategoryAttribute("code")> _
	<System.Xml.Serialization.XmlTypeAttribute(AnonymousType := True, [Namespace] := "http://www.ahmsa.com/xsd/AddendaAHM1")> _
	Public Partial Class AddendaAHMDocumentoDetalleLiquidacion

		Private fechaInicioField As String

		Private fechaFinField As String

		Private Shared m_serializer As System.Xml.Serialization.XmlSerializer

		<System.Xml.Serialization.XmlAttributeAttribute> _
		Public Property FechaInicio() As String
			Get
				Return Me.fechaInicioField
			End Get
			Set
				Me.fechaInicioField = value
			End Set
		End Property

		<System.Xml.Serialization.XmlAttributeAttribute> _
		Public Property FechaFin() As String
			Get
				Return Me.fechaFinField
			End Get
			Set
				Me.fechaFinField = value
			End Set
		End Property

		Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
			Get
				If (m_serializer Is Nothing) Then
					m_serializer = New System.Xml.Serialization.XmlSerializer(GetType(AddendaAHMDocumentoDetalleLiquidacion))
				End If
				Return m_serializer
			End Get
		End Property

		#Region "Serialize/Deserialize"
		''' <summary>
		''' Serializes current AddendaAHMDocumentoDetalleLiquidacion object into an XML document
		''' </summary>
		''' <returns>string XML value</returns>
		Public Overridable Function Serialize(encoding As System.Text.Encoding) As String
			Dim streamReader As System.IO.StreamReader = Nothing
			Dim memoryStream As System.IO.MemoryStream = Nothing
			Try
				memoryStream = New System.IO.MemoryStream()
				Dim xmlWriterSettings As New System.Xml.XmlWriterSettings()
				xmlWriterSettings.Encoding = encoding
				Dim xmlWriter__1 As System.Xml.XmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings)
				Serializer.Serialize(xmlWriter__1, Me)
				memoryStream.Seek(0, System.IO.SeekOrigin.Begin)
				streamReader = New System.IO.StreamReader(memoryStream)
				Return streamReader.ReadToEnd()
			Finally
				If (streamReader IsNot Nothing) Then
					streamReader.Dispose()
				End If
				If (memoryStream IsNot Nothing) Then
					memoryStream.Dispose()
				End If
			End Try
		End Function

		Public Overridable Function Serialize() As String
			Return Serialize(Encoding.UTF8)
		End Function

		''' <summary>
		''' Deserializes workflow markup into an AddendaAHMDocumentoDetalleLiquidacion object
		''' </summary>
		''' <param name="xml">string workflow markup to deserialize</param>
		''' <param name="obj">Output AddendaAHMDocumentoDetalleLiquidacion object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function Deserialize(xml As String, obj As AddendaAHMDocumentoDetalleLiquidacion, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = Deserialize(xml)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function Deserialize(xml As String, obj As AddendaAHMDocumentoDetalleLiquidacion) As Boolean
			Dim exception As System.Exception = Nothing
			Return Deserialize(xml, obj, exception)
		End Function

		Public Shared Function Deserialize(xml As String) As AddendaAHMDocumentoDetalleLiquidacion
			Dim stringReader As System.IO.StringReader = Nothing
			Try
				stringReader = New System.IO.StringReader(xml)
				Return DirectCast(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), AddendaAHMDocumentoDetalleLiquidacion)
			Finally
				If (stringReader IsNot Nothing) Then
					stringReader.Dispose()
				End If
			End Try
		End Function

		''' <summary>
		''' Serializes current AddendaAHMDocumentoDetalleLiquidacion object into file
		''' </summary>
		''' <param name="fileName">full path of outupt xml file</param>
		''' <param name="exception">output Exception value if failed</param>
		''' <returns>true if can serialize and save into file; otherwise, false</returns>
		Public Overridable Function SaveToFile(fileName As String, encoding As System.Text.Encoding, exception As System.Exception) As Boolean
			exception = Nothing
			Try
				SaveToFile(fileName, encoding)
				Return True
			Catch e As System.Exception
				exception = e
				Return False
			End Try
		End Function

		Public Overridable Function SaveToFile(fileName As String, exception As System.Exception) As Boolean
			Return SaveToFile(fileName, Encoding.UTF8, exception)
		End Function

		Public Overridable Sub SaveToFile(fileName As String)
			SaveToFile(fileName, Encoding.UTF8)
		End Sub

		Public Overridable Sub SaveToFile(fileName As String, encoding__1 As System.Text.Encoding)
			Dim streamWriter As System.IO.StreamWriter = Nothing
			Try
				Dim xmlString As String = Serialize(encoding__1)
				streamWriter = New System.IO.StreamWriter(fileName, False, Encoding.UTF8)
				streamWriter.WriteLine(xmlString)
				streamWriter.Close()
			Finally
				If (streamWriter IsNot Nothing) Then
					streamWriter.Dispose()
				End If
			End Try
		End Sub

		''' <summary>
		''' Deserializes xml markup from file into an AddendaAHMDocumentoDetalleLiquidacion object
		''' </summary>
		''' <param name="fileName">string xml file to load and deserialize</param>
		''' <param name="obj">Output AddendaAHMDocumentoDetalleLiquidacion object</param>
		''' <param name="exception">output Exception value if deserialize failed</param>
		''' <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding, obj As AddendaAHMDocumentoDetalleLiquidacion, exception As System.Exception) As Boolean
			exception = Nothing
			obj = Nothing
			Try
				obj = LoadFromFile(fileName, encoding)
				Return True
			Catch ex As System.Exception
				exception = ex
				Return False
			End Try
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHMDocumentoDetalleLiquidacion, exception As System.Exception) As Boolean
			Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String, obj As AddendaAHMDocumentoDetalleLiquidacion) As Boolean
			Dim exception As System.Exception = Nothing
			Return LoadFromFile(fileName, obj, exception)
		End Function

		Public Shared Function LoadFromFile(fileName As String) As AddendaAHMDocumentoDetalleLiquidacion
			Return LoadFromFile(fileName, Encoding.UTF8)
		End Function

		Public Shared Function LoadFromFile(fileName As String, encoding As System.Text.Encoding) As AddendaAHMDocumentoDetalleLiquidacion
			Dim file As System.IO.FileStream = Nothing
			Dim sr As System.IO.StreamReader = Nothing
			Try
				file = New System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read)
				sr = New System.IO.StreamReader(file, encoding)
				Dim xmlString As String = sr.ReadToEnd()
				sr.Close()
				file.Close()
				Return Deserialize(xmlString)
			Finally
				If (file IsNot Nothing) Then
					file.Dispose()
				End If
				If (sr IsNot Nothing) Then
					sr.Dispose()
				End If
			End Try
		End Function
		#End Region
	End Class
End Namespace