Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports Org.BouncyCastle.Security
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.OpenSsl

Namespace uCFDsLib
	Public Class Llave
		Private _RutaKey As String
		Private _Password As String

		''' <summary>
		''' Inicializa la clase indicando la ruta del archivo *.key y su password
		''' </summary>
		''' <param name="RutaLlave">Especifica la ruta del archivo *.key</param>
		''' <param name="Password">Especifica el password para abrir el archivo *.key</param>
		Public Sub New(ByVal RutaLlave As String, ByVal Password As String)
			_RutaKey = RutaLlave
			_Password = Password

			If File.Exists(RutaLlave) Then
				Dim dataKey() As Byte = File.ReadAllBytes(RutaLlave)
				Try
					Dim asp As Org.BouncyCastle.Crypto.AsymmetricKeyParameter = Org.BouncyCastle.Security.PrivateKeyFactory.DecryptKey(Password.ToCharArray(), dataKey)
				Catch ex As Exception
					Throw New Exception("La clave del archivo no es válida. Error: " & ex.Message)
				End Try
			Else
				Throw New Exception("El archivo llave no existe en el directorio especificado")
			End If
		End Sub

		''' <summary>
		''' Genera el sello a patir de una cadena original
		''' </summary>
		''' <param name="CadenaOriginal">Recibe el parametro de la cadena original</param>
		''' <returns>Regresa el sello digital</returns>
		Public Function GenerarSello(ByVal CadenaOriginal As String) As String
			Try
				Dim pass As String = _Password
				Dim dataKey() As Byte = File.ReadAllBytes(_RutaKey)
				Dim asp As Org.BouncyCastle.Crypto.AsymmetricKeyParameter = Org.BouncyCastle.Security.PrivateKeyFactory.DecryptKey(pass.ToCharArray(), dataKey)
				Dim ms As New MemoryStream()
				Dim writer As TextWriter = New StreamWriter(ms)
				Dim stWrite As New System.IO.StringWriter()
				Dim pmw As Org.BouncyCastle.OpenSsl.PemWriter = New PemWriter(stWrite)
				pmw.WriteObject(asp)
				stWrite.Close()

				'ISigner sig = SignerUtilities.GetSigner("MD5WithRSAEncryption");
				Dim sig As ISigner = SignerUtilities.GetSigner("SHA1WithRSA")

				'' Convertir a UTF8
				Dim plaintext() As Byte = Encoding.UTF8.GetBytes(CadenaOriginal)

				'' SELLAR
				sig.Init(True, asp)
				sig.BlockUpdate(plaintext, 0, plaintext.Length)
				Dim signature() As Byte = sig.GenerateSignature()

				Dim signatureHeader As Object = Convert.ToBase64String(signature)
				Return signatureHeader.ToString()
			Catch ex As Exception
				Throw New Exception("Ocurrió un error al intentar generar el sello. Error: " & ex.Message)
			End Try
		End Function
	End Class
End Namespace
