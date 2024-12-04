Imports System.IO

Partial Public Class estatus
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call ReporteAlmacenamiento()
    End Sub

    Private Sub ReporteAlmacenamiento()
        Dim espacioutilizado As Decimal = 0
        lblEspacioMaximo.Text = (System.Configuration.ConfigurationManager.AppSettings("espacio_maximo") / 1024 / 1024).ToString
        espacioutilizado = FormatNumber((GetFolderSize(Server.MapPath("~/portalcfd/cfd_storage"), False) / 1024 / 1024) + (GetFolderSize(Server.MapPath("~/portalcfd/pdf"), False) / 1024 / 1024), 2)
        lblEspacioUtilizado.Text = espacioutilizado.ToString
        lblEspacioDisponible.Text = ((System.Configuration.ConfigurationManager.AppSettings("espacio_maximo") / 1024 / 1024) - espacioutilizado).ToString
        RadRadialGauge1.Scale.Max = System.Configuration.ConfigurationManager.AppSettings("espacio_maximo") / 1024 / 1024
        RadRadialGauge1.Pointer.Value = espacioutilizado

    End Sub

    Function GetFolderSize(ByVal DirPath As String, Optional ByVal IncludeSubFolders As Boolean = True) As Long

        Dim lngDirSize As Long
        Dim objFileInfo As FileInfo
        Dim objDir As DirectoryInfo = New DirectoryInfo(DirPath)
        Dim objSubFolder As DirectoryInfo

        Try

            'add length of each file
            For Each objFileInfo In objDir.GetFiles()
                lngDirSize += objFileInfo.Length
            Next

            'call recursively to get sub folders
            'if you don't want this set optional
            'parameter to false 
            If IncludeSubFolders Then
                For Each objSubFolder In objDir.GetDirectories()
                    lngDirSize += GetFolderSize(objSubFolder.FullName)
                Next
            End If

        Catch Ex As Exception


        End Try

        Return lngDirSize
    End Function

End Class