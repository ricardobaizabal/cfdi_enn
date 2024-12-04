Imports Microsoft.VisualBasic
Imports System.Net.Mail
Public Class ObjComms
    Private _EmailTo As String = ""
    Private _EmailFrom As String = ""
    Private _EmailSubject As String = ""
    Private _EmailBody As String = ""
    Private _EmailCC As String = ""
    WriteOnly Property EmailTo() As String
        Set(ByVal value As String)
            _EmailTo = value
        End Set
    End Property
    WriteOnly Property EmailCc() As String
        Set(ByVal value As String)
            _EmailCC = value
        End Set
    End Property
    WriteOnly Property EmailFrom() As String
        Set(ByVal value As String)
            _EmailFrom = value
        End Set
    End Property
    WriteOnly Property EmailSubject() As String
        Set(ByVal value As String)
            _EmailSubject = value
        End Set
    End Property
    WriteOnly Property EmailBody() As String
        Set(ByVal value As String)
            _EmailBody = value
        End Set
    End Property
    Public Sub EmailSend()
        '
        '
        Dim objMM As New MailMessage
        objMM.To.Add(_EmailTo)
        If _EmailCC.Length > 0 Then
            objMM.CC.Add(_EmailCC)
        End If
        objMM.From = New MailAddress(_EmailFrom, _EmailFrom)
        objMM.IsBodyHtml = True
        objMM.Priority = MailPriority.Normal
        objMM.Subject = _EmailSubject
        objMM.Body = _EmailBody
        '
        '
        Dim SmtpMail As New SmtpClient
        Try
            Dim SmtpUser As New Net.NetworkCredential
            SmtpUser.UserName = "enviosweb@linkium.mx"
            SmtpUser.Password = "Link2020"
            SmtpMail.UseDefaultCredentials = False
            SmtpMail.Credentials = SmtpUser
            SmtpMail.Host = "smtp.linkium.mx"
            'SmtpMail.Host = "zimba.linkium.net"
            'SmtpMail.Port = 465
            SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network
            'SmtpMail.EnableSsl = True
            SmtpMail.Send(objMM)
        Catch ex As Exception
            '
            '
        Finally
            SmtpMail = Nothing
        End Try
        objMM = Nothing
        '
        '
    End Sub
End Class
