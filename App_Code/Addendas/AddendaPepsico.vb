﻿Imports System
Imports System.Diagnostics
Imports System.Xml.Serialization
Imports System.Collections
Imports System.Xml.Schema
Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Collections.Generic
'------------------------------------------------------------------------------
' <auto-generated>
'     Este código fue generado por una herramienta.
'     Versión del motor en tiempo de ejecución:2.0.50727.8000
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código.
' </auto-generated>
'------------------------------------------------------------------------------
Namespace uAddendas.AddendaPepsico

    '
    'This source code was auto-generated by xsd, Version=2.0.50727.3038.
    '

    '''<comentarios/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code")> _
    Partial Public Class RequestCFD

        Private Shared m_serializer As System.Xml.Serialization.XmlSerializer

        Private documentoField() As RequestCFDDocumento

        Private proveedorField() As RequestCFDProveedor

        Private recepcionesField() As RequestCFDRecepcionesRecepcion

        Private tipoField As String

        Private versionField As String

        Private idPedidoField As String

        Private idSolicitudPagoField As String

        '''<comentarios/>
        <System.Xml.Serialization.XmlElementAttribute("Documento")> _
        Public Property Documento() As RequestCFDDocumento()
            Get
                Return Me.documentoField
            End Get
            Set(ByVal value As RequestCFDDocumento())
                Me.documentoField = value
            End Set
        End Property

        '''<comentarios/>
        <System.Xml.Serialization.XmlElementAttribute("Proveedor")> _
        Public Property Proveedor() As RequestCFDProveedor()
            Get
                Return Me.proveedorField
            End Get
            Set(ByVal value As RequestCFDProveedor())
                Me.proveedorField = value
            End Set
        End Property

        '''<comentarios/>
        ''' 

        <System.Xml.Serialization.XmlArrayItemAttribute("Recepcion")> _
        Public Property Recepciones() As RequestCFDRecepcionesRecepcion()
            Get
                Return Me.recepcionesField
            End Get
            Set(ByVal value As RequestCFDRecepcionesRecepcion())
                Me.recepcionesField = value
            End Set
        End Property

        '''<comentarios/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property tipo() As String
            Get
                Return Me.tipoField
            End Get
            Set(ByVal value As String)
                Me.tipoField = value
            End Set
        End Property

        '''<comentarios/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property version() As String
            Get
                Return Me.versionField
            End Get
            Set(ByVal value As String)
                Me.versionField = value
            End Set
        End Property

        '''<comentarios/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property idPedido() As String
            Get
                Return Me.idPedidoField
            End Get
            Set(ByVal value As String)
                Me.idPedidoField = value
            End Set
        End Property

        '''<comentarios/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property idSolicitudPago() As String
            Get
                Return Me.idSolicitudPagoField
            End Get
            Set(ByVal value As String)
                Me.idSolicitudPagoField = value
            End Set
        End Property

#Region "Serialize/Deserialize"
        Private Shared ReadOnly Property Serializer() As System.Xml.Serialization.XmlSerializer
            Get
                If (m_serializer Is Nothing) Then
                    m_serializer = New System.Xml.Serialization.XmlSerializer(GetType(RequestCFD))
                End If
                Return m_serializer
            End Get
        End Property
        ''' <summary>
        ''' Serializes current AddendaAHMDocumentoDetalleLiquidacion object into an XML document
        ''' </summary>
        ''' <returns>string XML value</returns>
        Public Overridable Function Serialize(ByVal encoding As System.Text.Encoding) As String
            Dim ns As XmlSerializerNamespaces = New XmlSerializerNamespaces()
            ns.Add("", "")
            Dim streamReader As System.IO.StreamReader = Nothing
            Dim memoryStream As System.IO.MemoryStream = Nothing
            Try
                memoryStream = New System.IO.MemoryStream()
                Dim xmlWriterSettings As New System.Xml.XmlWriterSettings()
                xmlWriterSettings.Encoding = encoding
                Dim xmlWriter__1 As System.Xml.XmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings)
                Serializer.Serialize(xmlWriter__1, Me, ns)
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
        Public Shared Function Deserialize(ByVal xml As String, ByVal obj As RequestCFD, ByVal exception As System.Exception) As Boolean
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

        Public Shared Function Deserialize(ByVal xml As String, ByVal obj As RequestCFD) As Boolean
            Dim exception As System.Exception = Nothing
            Return Deserialize(xml, obj, exception)
        End Function

        Public Shared Function Deserialize(ByVal xml As String) As RequestCFD
            Dim stringReader As System.IO.StringReader = Nothing
            Try
                stringReader = New System.IO.StringReader(xml)
                Return DirectCast(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader)), RequestCFD)
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
        Public Overridable Function SaveToFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByVal exception As System.Exception) As Boolean
            exception = Nothing
            Try
                SaveToFile(fileName, encoding)
                Return True
            Catch e As System.Exception
                exception = e
                Return False
            End Try
        End Function

        Public Overridable Function SaveToFile(ByVal fileName As String, ByVal exception As System.Exception) As Boolean
            Return SaveToFile(fileName, Encoding.UTF8, exception)
        End Function

        Public Overridable Sub SaveToFile(ByVal fileName As String)
            SaveToFile(fileName, Encoding.UTF8)
        End Sub

        Public Overridable Sub SaveToFile(ByVal fileName As String, ByVal encoding__1 As System.Text.Encoding)
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
        Public Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding, ByVal obj As RequestCFD, ByVal exception As System.Exception) As Boolean
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

        Public Shared Function LoadFromFile(ByVal fileName As String, ByVal obj As RequestCFD, ByVal exception As System.Exception) As Boolean
            Return LoadFromFile(fileName, Encoding.UTF8, obj, exception)
        End Function

    Public Shared Function LoadFromFile(ByVal fileName As String, ByVal obj As RequestCFD) As Boolean
        Dim exception As System.Exception = Nothing
        Return LoadFromFile(fileName, obj, exception)
    End Function

    Public Shared Function LoadFromFile(ByVal fileName As String) As RequestCFD
        Return LoadFromFile(fileName, Encoding.UTF8)
    End Function

    Public Shared Function LoadFromFile(ByVal fileName As String, ByVal encoding As System.Text.Encoding) As RequestCFD
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

    '''<comentarios/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code")> _
    Partial Public Class RequestCFDDocumento

        Private referenciaField As String

        Private serieField As String

        Private folioField As String

        Private folioUUIDField As String

        Private tipoDocField As String

        '''<comentarios/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property referencia() As String
            Get
                Return Me.referenciaField
            End Get
            Set(ByVal value As String)
                Me.referenciaField = value
            End Set
        End Property

        '''<comentarios/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property serie() As String
            Get
                Return Me.serieField
            End Get
            Set(ByVal value As String)
                Me.serieField = value
            End Set
        End Property

        '''<comentarios/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property folio() As String
            Get
                Return Me.folioField
            End Get
            Set(ByVal value As String)
                Me.folioField = value
            End Set
        End Property

        '''<comentarios/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property folioUUID() As String
            Get
                Return Me.folioUUIDField
            End Get
            Set(ByVal value As String)
                Me.folioUUIDField = value
            End Set
        End Property

        '''<comentarios/>
        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer")> _
        Public Property tipoDoc() As String
            Get
                Return Me.tipoDocField
            End Get
            Set(ByVal value As String)
                Me.tipoDocField = value
            End Set
        End Property
    End Class

    '''<comentarios/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code")> _
    Partial Public Class RequestCFDProveedor

        Private idProveedorField As String

        '''<comentarios/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property idProveedor() As String
            Get
                Return Me.idProveedorField
            End Get
            Set(ByVal value As String)
                Me.idProveedorField = value
            End Set
        End Property
    End Class

    '''<comentarios/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code")> _
    Partial Public Class RequestCFDRecepcionesRecepcion

        Private conceptoField() As RequestCFDRecepcionesRecepcionConcepto

        Private idRecepcionField As String

        '''<comentarios/>
        <System.Xml.Serialization.XmlElementAttribute("Concepto")> _
        Public Property Concepto() As RequestCFDRecepcionesRecepcionConcepto()
            Get
                Return Me.conceptoField
            End Get
            Set(ByVal value As RequestCFDRecepcionesRecepcionConcepto())
                Me.conceptoField = value
            End Set
        End Property

        '''<comentarios/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property idRecepcion() As String
            Get
                Return Me.idRecepcionField
            End Get
            Set(ByVal value As String)
                Me.idRecepcionField = value
            End Set
        End Property
    End Class

    '''<comentarios/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code")> _
    Partial Public Class RequestCFDRecepcionesRecepcionConcepto

        Private cantidadField As Decimal

        Private unidadField As String

        Private descripcionField As String

        Private valorUnitarioField As Decimal

        Private importeField As Decimal

        '''<comentarios/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property cantidad() As Decimal
            Get
                Return Me.cantidadField
            End Get
            Set(ByVal value As Decimal)
                Me.cantidadField = value
            End Set
        End Property

        '''<comentarios/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property unidad() As String
            Get
                Return Me.unidadField
            End Get
            Set(ByVal value As String)
                Me.unidadField = value
            End Set
        End Property

        '''<comentarios/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property descripcion() As String
            Get
                Return Me.descripcionField
            End Get
            Set(ByVal value As String)
                Me.descripcionField = value
            End Set
        End Property

        '''<comentarios/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property valorUnitario() As Decimal
            Get
                Return Me.valorUnitarioField
            End Get
            Set(ByVal value As Decimal)
                Me.valorUnitarioField = value
            End Set
        End Property

        '''<comentarios/>
        <System.Xml.Serialization.XmlAttributeAttribute()> _
        Public Property importe() As Decimal
            Get
                Return Me.importeField
            End Get
            Set(ByVal value As Decimal)
                Me.importeField = value
            End Set
        End Property



    End Class

    '''<comentarios/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code")> _
    Partial Public Class NewDataSet

        Private itemsField() As RequestCFD

        '''<comentarios/>
        <System.Xml.Serialization.XmlElementAttribute("RequestCFD")> _
        Public Property Items() As RequestCFD()
            Get
                Return Me.itemsField
            End Get
            Set(ByVal value As RequestCFD())
                Me.itemsField = value
            End Set
        End Property


    End Class

End Namespace