Imports System.Xml

Public Class Lector
    Private _cfdv32 As Cfdi32.Comprobante
    Private _mensajeError As String
    Private _rutaDocumento As String

    Public ReadOnly Property Cfdv32() As Cfdi32.Comprobante
        Get
            Return _cfdv32
        End Get
    End Property

    Public ReadOnly Property MensajeError() As String
        Get
            Return _mensajeError
        End Get
    End Property

    Public Function LeerVersion32() As Boolean
        Try
            _cfdv32 = Cfdi32.Comprobante.LoadFromFile(_rutaDocumento)
            If _cfdv32 IsNot Nothing Then
                Return True
            Else
                _mensajeError = "No se pudo leer el documento"
                Return False
            End If
        Catch ex As Exception
            _mensajeError = ex.Message
            Return False
        End Try
    End Function

    Public Function LeerXML(ByRef rutaDocumento As String) As Boolean
        Dim xmlDoc As XmlDocument
        Dim elemento As XmlElement
        Dim version As String
        Try
            If System.IO.File.Exists(rutaDocumento) Then
                xmlDoc = New XmlDocument()
                xmlDoc.Load(rutaDocumento)

                elemento = xmlDoc.DocumentElement
                If elemento.GetAttribute("version") IsNot Nothing Then
                    version = elemento.GetAttribute("version")
                    _rutaDocumento = rutaDocumento

                    If version.Equals("3.2") Then
                        Return LeerVersion32()
                    Else
                        _mensajeError = "Version no soportada"
                        Return False
                    End If
                Else
                    _mensajeError = "No se encontró el atributo version"
                    Return False
                End If
            Else
                _mensajeError = "El archivo no existe en la ruta especificada"
                Return False
            End If
        Catch ex As Exception
            _mensajeError = ex.Message
            Return False
        End Try
    End Function

End Class
