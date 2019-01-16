<%
On Error Resume Next
Server.ScriptTimeOut  = 7200


%>
<title>SPAMMER PRIV8 - {Awake Edition} //html by Mental_way</title>
<body bgcolor="#C0C0C0" style="text-align: center">
<% 
Select case request("sys")

case "1"
Componente = "CDOSYS"

case "2"
Componente = "ABMailer"

case "3"
Componente = "Bamboo"

case "4"
Componente = "PersitsASPMail"

case "5"
Componente = "JMail.SMTPMail"

case "6"
Componente = "JMail.Message"

case "7"
Componente = "JMail4"

case "8"
Componente = "CDONTS"

case "9"
Componente = "CDO"

case else 
request("sys") = 9
Componente = "CDO"

end select
%>
<p> <b><font color="#000080" face="Tahoma">SPAMMER PRIV8 - <%=Componente%></font></b></p>
<%
if request("botao") = "enviar" then


Session("Remetente") = request("remetente")
Session("Remetente2") = request("remetente2")
Session("Assunto") = request("subject")
Session("HTML") = request("body")

 	vetordelinhas = Split(Request.Form("to"),chr(13))

	For i = 0 To UBound(vetordelinhas)
	
	
		on error resume next 
	Enviar request("remetente2"), request("remetente"), vetordelinhas(i), request("subject"), request("body"), request("sys")
	
        	If Err.Number = 0 Then
            	Response.Write "<br><b><font face='Tahoma' size='2' color='green'>[ " & i+1 & " ] Enviado para: " & vetordelinhas(i) & "</font>"
			Else
            	Response.Write "<br><font face='Tahoma' size='2' color='red'><b>[ " & i & " ] FaLhOu EM: " & vetordelinhas(i) & " - ERRO: " & Err.Description & "</font>"
			End If
	Next

	Function Enviar(FromName, From, rcpt, subject, body, scEmailComObj)
on error resume next
if scEmailComObj=1 then
	Dim mail 
	Dim iConf 
	Dim Flds
	on error resume next 
	Set mail = CreateObject("CDO.Message") 'calls CDO message COM object
	Set iConf = CreateObject("CDO.Configuration") 'calls CDO configuration COM object
	Set Flds = iConf.Fields
	Flds( "http://schemas.microsoft.com/cdo/configuration/sendusing") = 1 'tells cdo we're using the local smtp service, use "2" if not local
	Flds("http://schemas.microsoft.com/cdo/configuration/smtpserver")=scSMTP
	Flds("http://schemas.microsoft.com/cdo/configuration/smtpserverport") = 25
	Flds("http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout") = 20
	Flds("http://schemas.microsoft.com/cdo/configuration/smtpserverpickupdirectory") = "c:\inetpub\mailroot\pickup" 'verify that this path is correct
	Flds.Update 'updates CDO's configuration database
	'if smtp authentication is required
	'==================================
	if scSMTPAuthentication="Y" then
		Flds("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate") = 1 ' cdoBasic 
		Flds("http://schemas.microsoft.com/cdo/configuration/sendusername") = scSMTPUID
		Flds("http://schemas.microsoft.com/cdo/configuration/sendpassword") = scSMTPPWD
		Flds.Update 'updates CDO's configuration database
	end if
'==================================
	Set mail.Configuration = iConf 'sets the configuration for the message
	mail.To = rcpt 
	mail.From = chr(34) & FromName &  chr(34) & " " & from 
	mail.Subject = subject 
	
			HTMLBody=HTML
			mail.HTMLBody = body

	
	mail.Send 'commands CDO to send the message
	set mail=nothing 
End If

if scEmailComObj=2 then
	on error resume next 
	objMail.clear
	set objMail = Server.CreateObject("ABMailer.Mailman")
	objMail.ServerAddr = scSMTP
	'if authentication is used
	if scSMTPAuthentication="Y" then
		objMail.ServerPort = scPort
		objMail.ServerLoginUserID = scSMTPUID
	end if
	objMail.SendTo = rcpt 
	objMail.ReplyTo = from
	objMail.MailSubject = subject
	objMail.MailMessage = body
	objMail.SendMail 
	set objMail=nothing 
End If

if scEmailComObj=3 then
	on error resume next 
	set objMail = Server.CreateObject("Bamboo.SMTP") 
	objMail.Server = scSMTP
	objMail.Rcpt = rcpt 
	objMail.From = from
	objMail.FromName = fromName
	objMail.Subject = subject
	
	objMail.ContentType = "text/html"

	objMail.Message = body
	objMail.Send
	set objMail=nothing 
End If


if scEmailComObj=4 then
	on error resume next 
	
	'session.codepage = 65001 'UTF-8 code  'uncomment for UTF-8 code
	set objMail 	= server.CreateObject("Persits.MailSender")
	
	objMail.Host 	= scSMTP
	'if authentication is used
	if scSMTPAuthentication="Y" then
		objMail.Username = scSMTPUID
		objMail.Password = scSMTPPWD
	end if
	objMail.From 	= from
	objMail.FromName = fromName 'comment out for UTF-8 code
	'objMail.FromName 	= objMail.EncodeHeader(fromName,"utf-8")  'uncomment for UTF-8 code
	objMail.AddAddress rcpt
	objMail.AddReplyTo from
	objMail.Subject = subject  'comment out for UTF-8 code
	'objMail.Subject 	= objMail.EncodeHeader(subject, "utf-8")  'uncomment for UTF-8 code
	objMail.Body 	= body
	
	'UTF-8 parameters
	'objMail.CharSet = "UTF-8" 'uncomment for UTF-8 code
	'objMail.ContentTransferEncoding = "Quoted-Printable" 'uncomment for UTF-8 code
	
	
		objMail.IsHTML = True
	
	objMail.Send
	set objMail=nothing 
End If

if scEmailComObj=5 then
	on error resume next 
 	Set objMail = Server.CreateObject("JMail.SMTPMail")
  'objMail.ServerAddress=scSMTP
	objMail.Sender=from
	objMail.SenderName=fromName
	objMail.Subject= subject
	
		objMail.ContentType = "text/html"

	objMail.AddRecipient rcpt
	objMail.Body	= body
	objMail.Priority = 3
	objMail.Execute
	set objMail=nothing 
End If

if scEmailComObj=6 then
	on error resume next 
 	Set objMail=Server.CreateOBject( "JMail.Message" )
	objMail.Logging = true
	objMail.silent = true
	'if authentication is used
	if scSMTPAuthentication="Y" then
		objMail.MailServerPassword=scSMTPPWD
		objMail.MailServerUserName=scSMTPUID
	end if
	objMail.From = from
	objMail.FromName = fromName
	objMail.AddRecipient rcpt, rcpt
	objMail.Subject = subject
	objMail.Body = body
	if not objMail.Send(scSMTP) then
		'Response.write "<pre>" & objMail.log & "</pre>"
	end if
end if

if scEmailComObj=7 then
	on error resume next 
 set objMail = Server.CreateObject("SMTPsvg.Mailer")
 objMail.FromName=fromName
 objMail.FromAddress=from
 objMail.RemoteHost="localhost"
 objMail.AddRecipient rcpt, rcpt
 objMail.Subject=subject
	
	objMail.ContentType = "text/html"

 objMail.BodyText=body
 if objMail.SendMail then
  'Response.Write "Mail sent..."
 else
  'Response.Write "Mail send failure. Error was " & objMail.Response
  'Response.end
 end if
	set objMail=nothing 
End If

if scEmailComObj=8 then
	on error resume next 
	dim objMail
	Set objMail = Server.CreateObject ("CDONTS.NewMail")
	objMail.From = chr(34) & FromName &  chr(34) & " " & from 
	objMail.To   = rcpt
	objMail.Subject = subject
	objMail.Body    = body
	
	

		objMail.BodyFormat = 0
		objMail.MailFormat = 0
	
	
	objMail.Send
	
	set objMail=nothing 
End If

if scEmailComObj=9 then
	'on error resume next %>
	<!--METADATA TYPE="typelib" UUID="CD000000-8B95-11D1-82DB-00C04FB1625D" NAME="CDO for Windows Library" -->
	<!--METADATA TYPE="typelib" UUID="00000205-0000-0010-8000-00AA006D2EA4" NAME="ADODB Type Library" --> 
	<% 
	Const cdoSendUsingPort = 2
	Set objMail = Server.CreateObject("CDO.Message") 
	Set iConf = Server.CreateObject("CDO.Configuration")
	Set Flds = iConf.Fields 
	With Flds 
		.Item(cdoSendUsingMethod) = cdoSendUsingPort 
		if scSMTP<>"" then
			.Item(cdoSMTPServer) = scSMTP
		else
			.Item(cdoSMTPServer) = "mail-fwd"
		end if 
		.Item(cdoSMTPServerPort) = 25 
		.Item(cdoSMTPconnectiontimeout) = 10 
		'Only used if SMTP server requires Authentication
		if scSMTPAuthentication="Y" then
			.Item(cdoSMTPAuthenticate) = cdoBasic 
			.Item(cdoSendUserName) = scSMTPUID
			.Item(cdoSendPassword) = scSMTPPWD
		end if
		.Update 
	End With
	Set objMail.Configuration = iConf
	objMail.From = from 
	objMail.To = rcpt 
	objMail.Subject = Subject 
	objMail.TextBody = Body
	objMail.Send
	'Cleanup 
	Set objMail = Nothing 
	Set iConf = Nothing 
	Set Flds = Nothing 
End If


if scEmailComObj=10 then

 Set oEmail = Server.Createobject("Dynu.Email")
 REM Please set a valid SMTP server to be used to send the email message.
 oEmail.Host = "localhost"
 REM Set the to address. Multiple to addresses are supported.
 oEmail.AddAddress rcpt
 REM Set the cc addresses. Multiple cc addresses are supported.
 
 
 REM Set the bcc address. Multiple bcc addresses are supported.
 
 REM Set the subject of the email
 oEmail.Subject = subject
 REM Set the body of the email
 oEmail.Body = body
 REM Set a file as attachment
 'oEmail.AddAttachment "c:\test.txt" 
 REM Set the priority of message to HIGH(this step is optional)
 oEmail.SetPriority(0)
 REM Send the email message.
 On Error Resume Next
 If oEmail.Send() Then
      
 Else
      
 End If

 Set oEmail = nothing
 end if
 
  end function
end if

%>

<form method="POST" action="">
<table border="0" width="43%" height="434">
<tr>
<td width="13%" height="43"><strong>Remetente (Email):</strong></td>
<td width="87%" height="43">
<input type="text" value="<%= Session("Remetente")%>" name="remetente" size="72" style="font-size: 8pt; color: #FFFFFF; border: 1px solid #000080; background-color: #666666"></td>
</tr>
<tr>
<td width="13%" height="43"><strong>Remetente (Nome):</strong></td>
<td width="87%" height="43">
<input type="text" value="<%= Session("Remetente2")%>" name="remetente2" size="72" style="font-size: 8pt; color: #FFFFFF; border: 1px solid #000080; background-color: #666666"></td>
</tr>
<tr>
<td width="13%" height="43"><strong>Assunto:</strong></td>
<td width="87%" height="43">
<input type="text" value="<%= Session("Assunto")%>" name="subject" size="71" style="font-size: 8pt; color: #FFFFFF; border: 1px solid #000080; background-color: #666666"></td>
</tr>
<tr>
<td width="13%" height="170"><strong>Mensagem<br>
(HTML):</strong></td>
<td width="87%" height="170">
<textarea rows="12" name="body" cols="69" style="font-family: Tahoma; font-size: 8pt; color: #FFFFFF; border: 1px solid #000080; background-color: #666666"><%= Session("HTML")%></textarea></td>
</tr>
<tr>
<td width="13%" height="164"><strong>Lista De E-mails:</strong></td>
<td width="87%" height="164">
<textarea type="text" name="to" rows="15" cols="69"style="font-family: Tahoma; font-size: 8pt; color: #FFFFFF; border: 1px solid #000080; background-color: #666666">joeymrts@gmail.com</textarea></td>
</tr>
<tr>
<td width="13%" height="41"></td>
<td width="87%" height="41">
<p align="center"><input type="submit" value="enviar" name="botao" style="font-family: Tahoma; font-size: 8pt; color: #000000; border: 1px solid #000080; background-color: #C0C0C0; float:center"></td>
</tr>
</table>
</form>
<%
'Começamos o cronômetro!
starttime = Timer()

'Toda a página a seguir...
'Este loop só serve para alongar um pouco o processo
Do While tt < 30000
tt = tt + 1
Loop

'O que segue vai ao final da página.
'Observamos quanto tempo leva o cronômetro e salvamos o tempo final.
endtime = Timer()

'Mostramos os resultados obtidos.
Response.Write "O carregamento se completou em " & endtime-starttime & " segundos = "
Response.Write " (" & (endtime-starttime)*1000 & " milésimos de segundos)."
%> 
</font></body></html>
