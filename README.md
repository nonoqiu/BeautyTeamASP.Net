# Welcome to BeautyTeam API!

## What is BeautyTeam?
###Background
For more than thousands years, teamwork has always been an important work form. 3 to 30 people collaborate together to figure out a solution. By now, teamwork happens from kids in kindergarten to big enterprise with thousands of workers. Colloge students have to finish some work in teams. How can we take advantage at the new oppotunities and give all teams a reliable solution? BeautyTeam should not be a software, but a collaborate style. 
###From College to Corporation
We have experiences in college life, also touched common work life style. Lift style is busy and mixed.	
## Competitors
###Teambition  
[WebSite](https://www.teambition.com)  
Teambition is a greate team solution developed in China and has grown for years. Teambition is very successful in China, but somehow it doesn't fit some personal features.
###Teamwork
[WebSite](https://www.teamwork.com)  
Teamwork is also a greate team solution developed in US, which is very successful. But however...
###QQ
QQ is commonly used to convey messages.
###DingTalk
Ding talk is simply developed by ali.
## Core
Waiiting to be updated...  

##Server Class Design Map
![ClassDesign](http://www.soldo.com.cn/Public/UploadImage/mindmap.png)
## API
###Server Address
Our BeautyTeam project server works on
[www.obisoft.com.cn/api/](http://www.obisoft.com.cn/api/)    
Server current ip address is `123.56.238.114`, currently work by `IIS 8.5`, in `Windows Server 2012 R2`  
BeautyTeam project uses `URL Encode` to serialize object to post to the server.  
BeautyTeam project uses `HTTP` to transfer data.  
BeautyTeam project uses `JSON` to serialize or deserialize object.  
### Return Status Code  
We are just using HTTP status code as our api return status code.
For example, 404 means what you want to search can not be found.  
500 means the server crashed.
200 means what you meant was done successfully.  
**But not all http status code means it's meaning in http status code!**  
**All api may return these code! These common code may not repeat again in API Document**!  
200: Success  
500: Internal Server Error,  
406: Not Accept Value   
###Basic class
Commonly used class are designed in [Here](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels&_a=contents).  
`Server Reply`[View Details](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FBasicServer.cs&_a=contents)   
Server Reply is a common server reply class. All replied object inherits this class.    
 `ObiValue`[View Details](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FBasicServer.cs&_a=contents)   
ObiValue is a simple class which returns a value which may be a basic type or a struct.    
`ObiObject`[View Details](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FBasicServer.cs&_a=contents)   
ObiObject is a simple class which returns a object which may be an object.  
`INoticeable`[View Details](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FEventAndTask.cs&_a=contents)  
INoticeable is a simple interface which means that this object can be noticed in the user's notice list.  
`Event`[View Details](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FEventAndTask.cs&_a=contents)  
Event implements INoticeable, which is a simple class represents a event. Event have it's own happen time and end time, like a class or a meeting.  
`Task`[View Details](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FEventAndTask.cs&_a=contents)  
Task implements INoticeable, which is a simple class represents a Task. Task only have a deadline not like event.  
`Group`[View Details](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FGroup.cs&_a=contents)  
Group is an abstract class, which means some thing that user can join and create.  
  
`PersonalTask`[View Details](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FPersonal.cs&_a=contents)  
PersonalTask extends Task, which is a task that has no relationship with any group.  
`PersonalEvent`[View Details](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FPersonal.cs&_a=contents)    
PersonalEvent extends Event, which is an event that has no relationship with any group.  
  
`RadioStation`[View Details](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FRadioStation.cs&_a=contents)  
RadioStation extends Group, which is a group of people that do not work together but have the same tasks and events, like a math class, or an activity joiners.  
`RadioTask`[View Details](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FRadioStation.cs&_a=contents)  
RadioTask extends Task,  which is a task from a radiostation, like math homework.  
`RadioEvent`[View Details](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FRadioStation.cs&_a=contents)    
RadioEvent extends Event, which is an event from a radiostation, like math class in Friday afternoon.  
  
`Team`[View Details](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FTeam.cs&_a=contents)  
Team extends group, which is a group of people working together and focusing on many projects. For example, if our team name is Obisoft, our team projects contains VSpaceX and BeautyTeam. One team can focusing on lots of projects together.  
`Project`[View Details](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FTeam.cs&_a=contents)  
Project must be in a team. **Project does NOT has the definition of relation people, because team has team member, and tasks in projects have relation people already. **  
`GroupEvent`[View Details](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FTeam.cs&_a=contents)  
GroupEvent extends Event, which is an event that belongs to a team and have some relation people to do the group event together, like BeautyTeam developing team will have a meeting this Saturday.  
`GroupTask`[View Details](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FTeam.cs&_a=contents)  
GroupTask extends Task, which is a task that belongs to a team and have some relation people to do the task together, like BeuatyTeam developing team needs to finish the api docs before Saturday.  
**The difference between task and event is that task only has deadline while event has start time and end time**  

  



###Hello World API
Hello world API is a simple api which returns a string. If you can get this string, your config and connection to server is correct, and server is normally running.  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FBasicApiController.cs&_a=contents)    
	
	Address: /api
	Method: HTTP GET
	Argument:null  
	Return Value: ObiObject<string>  
	Authorize: None  
	
###Logging API
Logging API is a api to log client error when unhandled error happed in client like Android and iOS. If an unhandled error happened, use this API to upload the error infor for further analysition.  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FBasicApiController.cs&_a=contents)    
[Definition of EventLog](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FEventLog.cs&_a=contents)  
	
	Address: /api/Log  
	Method: HTTP POST   
    Post Content:EventLog
	Return Value: ServerReply  
	Authorize: None
	Possible Return Code Meaning: 200 = Success  
    
###Log in API
Log in API will change the current user state from unauthorized to authorized. User state can be hold for 20 minutes if nothing api called. Please use `cookie` to hold client user state. User state may be dropped down and please log in again after 20 minutes.  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FBasicApiController.cs&_a=contents)    
[Definition of LoginViewModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAccount%2FAccountViewModels.cs&_a=contents)  
	
	Address: /api/Login  
	Method: HTTP POST   
    Post Content:LoginViewModel
	Return Value: ObiObject<string>  
	Authorize: None
	Possible Return Code Meaning: 200 = Success,
								  403 = The account is locked out or log in failure,
								  302 = The account Requires Verification
								   	  
 
###Forgot Password API
This API used to deal the situation when user forget his password.  
**The Post Value is user's email. Server will send an email or send an sms message to the user's phone.**  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FBasicApiController.cs&_a=contents)  
[Definition of ServerReply](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FBasicServer.cs&_a=contents)  
[Definition of ForgotPasswordViewModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAccount%2FAccountViewModels.cs&_a=contents)
	
	Address: /api/ForgotPassword
	Method: HTTP POST
	Post Content: ForgotPasswordViewModel	
	Return Value: ServerReply
	Authorize: None
	Possible Return Code Meaning: 200 = Success,
								  404 = Not Found User  
								   


###Change Password API
This API is for user to change his password. The method is post. It need user's current password, new password and confirm password.The Post value class in server is ChangePasswordViewModel.  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FBasicApiController.cs&_a=contents)  
[Definition of ServerReply](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FBasicServer.cs&_a=contents) 
[Definition of ChangePasswordViewModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FManage%2FManageViewModels.cs&_a=contents)  
	
	Address: /api/ChangePassword
	Method: HTTP POST
	Post Content: ChangePasswordViewModel	
	Return Value: ServerReply
	Authorize: Logged in
	Possible Return Code Meaning: 200 = Success,
								  403 = Wrong Password



###Current User Information
Currentuser information will get information of currently logged in user, which includes many properties of an obisoft user. some attributes may not be implemented by Android or iOS and you can just skip them or use dynamic type.  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FBasicApiController.cs&_a=contents)    
[Definition of ObisoftUser](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FObisoftUser.cs&_a=contents)  
	
	Address: /api/currentuser  
	Method: HTTP GET
	Argument:null   
	Return Value: ObiObject<ObisoftUser>  
	Authorize: Logged in
	Possible Return Code Meaning: 200 = Success
								  
### Search Another User Info  
This API is for client to get a user's info by his id. The info is not include user's privacy.  
** id is user id **  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FBasicApiController.cs&_a=contents)  
[Definition of AnotherUser](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FObisoftUser.cs&_a=contents)  

	Address: /api/AnotherUser/5
	Method: HTTP GET
	Argument:id(Omitted)
	Return Value: ObiObject<AnotherUser>
	Authorize: None
	Possible Return Code Meaning: 200 = Success


###Register API  
Register API will register a new account. If the user registered successfully, the server will login automaticaly. The client don't need to log in again.  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FBasicApiController.cs&_a=contents)  
[Definition of ServerReply](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FBasicServer.cs&_a=contents)  
[Definition of RegisterViewModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAccount%2FAccountViewModels.cs&_a=contents)  

	Address: /api/Register  
	Method: HTTP POST
	Post Content: RegisterViewModel
	Return Value: ServerReply  
	Authorize: None
	Possible Return Code Meaning: 200 = Success
								  409 = Register failed. Maybe some user with the same Email has already registered.

###Login status API  
loginstatus API will return user's login status. 
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FBasicApiController.cs&_a=contents)  
[Definition of ObiValue](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FBasicServer.cs&_a=contents)  

	Address: /api/loginstatus  
	Method: HTTP GET
	Argument:null
	Return Value: ObiValue<bool>
	Authorize: None
	Possible Return Code Meaning: 200 = Success  
    
 ###Logoff API  
 logoff API will make the user log off. The cookie will be cleared.  
 [Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FBasicApiController.cs&_a=contents)  
 [Definition of ServerReply](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FBasicServer.cs&_a=contents)  

 	Address: /api/logoff
 	Method: HTTP POST
	Post Content: null	 
 	Return Value: ServerReply  
 	Authorize: Logged in
	Possible Return Code Meaning: 200 = Success  

 ###Latest version API  
 This API will return the latest software current version.  
 **it has two parameters. The method is get, So the parameter need to be append to the url. like this `https://www.obisoft.com.cn/api/latestversion?Platform=android&CurrentVersion=0.0.1`.**  
 **When the client call this kind api, It's important to do url encode. Because some special Symbol will make trouble. Also the data from server is be encoded.   So the client need to decode the data.**  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FBasicApiController.cs&_a=contents)  
[Definition of VersionChechResult](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FVersionCheckModel.cs&_a=contents)  

	Address: /api/latestversion
	Method: HTTP GET
	Argument: VersionCheckModel
	Return Value: VersionChechResult
	Authorize: None
	Possible Return Code Meaning: 200 = Success
								  404 = Not found your platform

###Find User Id through User's Email
This API can get user's Id through his email.  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FBasicApiController.cs&_a=contents)  

	Address: /api/FindUserByEmail  
	Method: HTTP GET
	Argument: Email
	Return Value: ObiObject<string>  
	Authorize: None
	Possible Return Code Meaning: 200 = Success
	
###Set User's Basic Info 
This API can set a user's basic info.  
[Source code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FBasicApiController.cs&_a=contents)  
[Definition of BasicInfoViewModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FManage%2FBasicInfoViewModel.cs&_a=contents)  

	Address: /api/SetBasicInfo  
	Method: HTTP POST  
	Argument: null
	Post Content: BasicInfoViewModel  
	Authorize: Logged in  
	Return Value: ServerReply  
	Possible Return Code Meaning: 200 = Success  	

###Upload a file like a image to our Obisoft OSS Service 
This API will upload a file to our server, and our server will upload the image to our oss service, then return the address of your file.  
**Caution: DO NOT Upload .EXE file! File should not larger than 4MB!**  
[Source code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FBasicApiController.cs&_a=contents)  
[Definition of UploadImageViewModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FUploadImageViewModel.cs&_a=contents)  

	Address: /api/UploadImage  
	Method: HTTP POST  
	Argument: null
	Post Content: UploadImageViewModel  
	Authorize: Logged in  
	Return Value: ObiObject<string>  
	Possible Return Code Meaning: 200 = Success  	



###School List API  
This API will get all the school list beauty team support.  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FSchoolApiController.cs&_a=contents)  
[Definition of ObiList<T>](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FBasicServer.cs&_a=contents)  
[Definition of School](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FServices%2FSchoolService.cs&_a=contents)  

	Address: /api/SchoolList
	Method: HTTP GET
	Argument:null
	Return Value: ObiList<School>
	Authorize: None
	Possible Return Code Meaning: 200 = Success

###School Detail API
This API will return a school's detail.  
 **The method is get, it has one parameter. The parameter is school's Id. the request url like this `/api/SchoolDetail/1`.**  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FSchoolApiController.cs&_a=contents)  
[Definition of School](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FServices%2FSchoolService.cs&_a=contents)  
[Definition of ObiObject<T>](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FBasicServer.cs&_a=contents)  

	Address: /api/SchoolDetail/5
	Method: HTTP GET
	Argument:id(omitted)
	Return Value: ObiObject<School>
	Authorize: None
	Possible Return Code Meaning: 200 = Success

###Bind School API
This API can bind a school and a user.  
**The method is post, but the post value is empty. The post url need append a parameter. The parameter is school Id.**  
**The request url like this `/api/BindSchool/1`**  
** id means school id **  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FSchoolApiController.cs&_a=contents)  
[Definition of ServerReply](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FBasicServer.cs&_a=contents)  

	Address：/api/BindSchool/5
	Method: HTTP POST
	Argument:id(omitted)
	Post Content: null
	Return Value: ServerReply
	Authorize: Logged in
	Possible Return Code Meaning: 200 = Success
								  404 = Can not find target school  

###Set School Account API
This API is used to set school account. It need student's Educational account and password.  
**The method is post.**  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FSchoolApiController.cs&_a=contents)  
[Definition of ServerReply](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FBasicServer.cs&_a=contents)  
[Definition of SetSchoolAccount](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FSetSchoolAccount.cs&_a=contents)  
	
	Address: /api/SetSchoolAccount
	Method: HTTP POST
	Post Content: SetSchoolAccount
	Return Value: ServerReply
	Authorize: Logged in And Already Binded School
	Possible Return Code Meaning: 200 = Success,
								  400 = Wrong Password,  
								  403 = Not binded school yet, can not set school account.   

###Get User's Team Statistic Info    
This api will get some interesting info about team.    
[Definition of TeamStatisticInfo](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FTeamStatisticInfo.cs&_a=contents)  

	Address：	/api/StatisticInfo
	method：	HTTP POST
	Argument：null
	return value：ObiObject<TeamStatisticInfo>
	Authorize: Logged in And Already Binded School
	Possible Return Code Meaning: 200 = Success,



###Create Team API  
This API is used to create a team. It need team name and team description. The post value class in server is CreateTeamModel.  
**The return value is the new team id.**    
**The method is post.**  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FTeamApiController.cs&_a=contents)  
[Definition of ServerReply](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FBasicServer.cs&_a=contents)  
[Definition of CreateTeamModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FBeautyTeam%2FCreateTeamModel.cs&_a=contents)  

	Address: /api/CreateTeam
	Method: HTTP POST
	Post Content: CreateTeamModel
	Return Value: ObiValue<int>
	Authorize: Logged in 
	Possible Return Code Meaning: 200 = Success,

###Create Radio Station
This API is used to create Radio Station. The method is post. It need RadioStationName and RadioStationDescription.  
The Post value class in server is CreateRadioStationModel. The return class is ServerReply. 
 **The return value is RadioStation id.**
 **Caution: The RadioStation and the Team are stored in the same chart in the Database, and team and radiostation are sharing the same GroupId!**
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FTeamApiController.cs&_a=contents)    
[Definition of CreateRadioStationModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FBeautyTeam%2FCreateRadioStationModel.cs&_a=contents)  

	Address: /api/CreateRadioStation
	Method: HTTP POST
	Post Content: CreateRadioStationModel 
	Return Value: ObiValue<int>
	Authorize: Logged in
	Possible Return Code Meaning: 200 = Success    						   	

###Delete Group
This API will delete the group. This API is the same as bind school api.  
The post content is null. Append the group Id to the url.  
** id means group id**  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FTeamApiController.cs&_a=contents)  

	Address: /api/DeleteGroup/5
	Method: HTTP POST
	Argument: id(omitted)
	Post Content: null
	Return Value: ServerReply 
	Authorize: Logged in and is the admin or the owner of the group
	Possible Return Code Meaning: 200 = Success
								  404 = Not found group
								  403 = Not Authorize(Only group admin or owner can delete group)
	
###Join Group 
This API is for user to join a team. The API call method like bind school API.  
** id means group id**  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FTeamApiController.cs&_a=contents)  

	Address: /api/JoinGroup/5
	Method: HTTP POST
	Argument: id(omitted)
	Post Content: null
	Return Value: ServerReply
	Authorize: logged in and not have joined the group
	Possible Return Code Meaning: 200 = Success,
								  404 = Not exist group,
								  409 = Conflict(This user already in this group)

###Leave Group
This API can make a user leave his group. The API call method like bind school API.  
** If the group owner leave the group, the group and another infomation about this group will be delete. The Client should warn the user, when this situation happened. **    
** id is group id **  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FTeamApiController.cs&_a=contents)  

	Address: /api/LeaveGroup/5
	Method: HTTP POST
	Argument: id(Omitted)
	Post Content: null
	Return Value: ServerReply
	Authorize: logged in and joined the group
	Possible Return Code Meaning: 200 = Success,
								  404 = Not exist group,
								  409 = Conflict(The user is not in this group)
						
###List User's Groups Joined or Created
This API is return all the group the user joined.  
** don't need any argument **  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FTeamApiController.cs&_a=contents)  
[Definition of GU_RelationR](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FGU_RelationR.cs&_a=contents)  

	Address: /api/Groupsijoined
	Method: HTTP GET
	Argument: null
	Return Value: ObiList<GU_RelationR>
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success  
	
###Make a user to be Admin in a group
This API is to make a user to be a Admin in group. It need two kinds of parameter. One is to append to an url, another is post content.  
**id is group id **  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FTeamApiController.cs&_a=contents)  
[Definition of SetGroupAdminModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FBeautyTeam%2FSetGroupAdminModel.cs&_a=contents)  

	Address: /api/SetAdmin/5
	Method: HTTP POST
	Argument: id(omitted)
	Post Content: SetGroupAdminModel
	Return Value: ServerReply
	Authorize: logged in and be the owner of the group and target user have joined the group and is not the owner or the admin of the group
	Possible Return Code Meaning: 200 = Success,
								  404 = Not Found this group
								  403 = Forbidden(The target user is not the group owner)
								  409 = Conflict(The target user is already owner or admin)
								  
								  
###Get Group Details
This API is to get a group details.  
** id is the group id **  
[Source code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FTeamApiController.cs&_a=contents)  
[Definition of Group](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FGroup.cs&_a=contents)  	

	Address: /api/GroupDetails/5
	Method: HTTP GET
	Argument: id(Omitted)
	Return Value: ObiObject<Group>  
	Authorize: None
	Possible Return Code Meaning: 200 = Success

###Get A team's projects  
This API can get a team's projects.  
** id is the team id **
[Source code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FTeamApiController.cs&_a=contents)  
[Definition of Project](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FTeam.cs&_a=contents)    	

	Address: /api/TeamProjects/5
	Method: HTTP GET  
	Argument: id(Omitted)
	Return Value: ObiList<Project>  
	Authorize: None  
	Possible Return Code Meaning: 200 = Success  
								  404 = Not found Team	
	
###BootUser
This API is make someone to leave this group.  
** id is group id **  

[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FTeamApiController.cs&_a=contents)   
[Definition of BootUserModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FBeautyTeam%2FBootUserModel.cs&_a=contents)  

	Address: /api/BootUser/5
	Method: HTTP POST
	Argument: id(Omitted)
	Post Content: null
	Return Value: ServerReply
	Authorize: logged in and be the owner or the admin of the group and the target user is not the admin if you are not the owner.
	Possible Return Code Meaning: 200 = Success,
								  404 = Not Found the group
								  403 = The current user is not a group owner or admin
								  409 = The current user is not owner but want to kick an admin

###Search a Group
This API can search a group by the keyword. If you set the bool 'isTeamNotRadioStation', result will only contains teams. Otherwise, only radio stations.   
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FTeamApiController.cs&_a=contents)   

	Address: /api/SearchGroup
	Method: HTTP GET
	Argument: string keyword, bool isTeamNotRadioStation
	Post Content: null
	Return Value: List<Group>
	Authorize: 
	Possible Return Code Meaning: 200 = Success,


###Create Project
This API is to make user to create project in the group. The call method like SetAdmin API.  
** id is team (group) id **  
** The return value is the new project id **  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FTeamApiController.cs&_a=contents)  
[Definition of CreateProjectModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FBeautyTeam%2FCreateProjectModel.cs&_a=contents)  

	Address: /api/CreateProject/5
	Method: HTTP POST
	Argument: id(Omitted)
	Post Content: CreateProjectModel
	Return Value: ObiValue<int>
	Authorize: logged in and is the admin or the owner of the group
	Possible Return Code Meaning: 200 = Success,
								  404 = Not exist group
								  403 = The current user is not group owner or admin
								  
								  
###Delete Project
This API is to delete a project in a group.  
** id is project id **  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FTeamApiController.cs&_a=contents)  

	Address: /api/DeleteProject/5
	Method: HTTP POST
	Argument: id(Omitted)
	Post Content: null
	Return Value: ServerReply
	Authorize: logged in and be an owner or an admin of current team
	Possible Return Code Meaning: 200 = Success,
								  404 = Not Found Project,
								  403 = Forbidden Result(Not Authorized)


								  
		
###Create Personal Task
This API is for user to create a new task.  
**In CreatePersonalTaskModel, NoticeBefore is not required. If user not give, server will save one day defautly. **  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FPersonalApiController.cs&_a=contents)  
[Definition of CreatePersonalTaskModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FBeautyTeam%2FCreatePersonalTaskModel.cs&_a=contents)  
[Definition of ObiValue<T>](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FBasicServer.cs&_a=contents)  
	
	Address: /api/CreatePersonalTask
	Method: HTTP POST
	Argument: null
	Post Content: CreatePersonalTaskModel
	Return Value: ObiValue<int>
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success
	
###Create Personal Event
This API is for user to create a new Event.  
** In CreatePersonalEventModel, NoticeBefore is not required, If user not give, Server will save 25 minutes defaultly.**  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FPersonalApiController.cs&_a=contents)  
[Definition of CreatePersonalEventModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FBeautyTeam%2FCreatePersonalEventModel.cs&_a=contents)  

	Address: /api/CreatePersonalEvent
	Method: HTTP POST
	Argument: null
	Post Content: CreatePersonalEventModel
	Return Value: ObiValue<int>
	AUthorize: logged in
	Possible Return Code Meaning: 200 = Success
	
	
###Delete Personal Task
This API will delete a user's task. Its call method like Join Group API.  
** id is task id **  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FPersonalApiController.cs&_a=contents)  

	Address: /api/DeletePersonalTask/5
	Method: HTTP POST
	Argument: id(omitted)
	Post Content: null
	Return Value: ServerReply
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success,
								  404 = Not Found Task
								  403 = Forbidden(The task's user id is not the current user's id)
								  
###Delete Personal Event
This API can delete a user's event. Its call method like join group API.  
** id is event id **  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FPersonalApiController.cs&_a=contents)  

	Address: /api/DeletePersonalEvent/5
	Method: HTTP POST  
	Argument: id(omitted)
	Post Content: null
	Return Value: ServerReply  
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success,
								  404 = Not Found this event
								  403 = Forbidden(The current user is not the event's user)
								  
###Personal Task Details
This API is to get a task details. The method is get, so the argument should append to the url.  
** id is task's id **  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FPersonalApiController.cs&_a=contents)  
[Definition of PersonalTask](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FPersonal.cs&_a=contents)  

	Address: /api/PersonalTaskDetails/5
	Method: HTTP GET
	Argument: id(Omitted)
	Return Value: ObiObject<PersonalTask>(If Success)
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success,
								  404 = Not Found this task,
								  403 = Forbidden(The task' owner is not the current user)
								   	 
###Personal Event Details
This APi is to get a user's event. Like the Get personal task details API.  
** id is event's id **  
[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FPersonalApiController.cs&_a=contents)  
[Definition of PersonalEvent](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FPersonal.cs&_a=contents)  

	Address: /api/PersonalEventDetails/5
	Method: HTTP GET
	Argument: id(Omitted)
	Return Value: ObiObject<PersonalEvent>(If Success)
	Authorize: logged in 
	Possible Return Code Meaning: 200 = Success,
								  404 = Not Found This Event
								  403 = Forbiddent(the event's owner is not the current user)  
								  
###Get All User's Notice  
This API Can get All user notice in time order. The time order is calulated by the time when task and event created gived.  

[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FPersonalApiController.cs&_a=contents)  
[Definition of ObiList](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAPI%2FBasicServer.cs&_a=contents)  
[Definition of INoticeable](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FEventAndTask.cs&_a=contents)  
	
	Address: /api/AllNoticeForMe 
	Method: HTTP GET
	Argument: null
	Return Value: ObiList<INoticeable>
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success  


###Get User's Notice  
This API can get a user's Notice. The Argument `amount` means the number of notice. The order is depend on the notice time. If not give amount, server will default set to 10.  

[Source Code of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FPersonalApiController.cs&_a=contents)  
  

	Address: /api/AllNoticeForMe  
	Method: HTTP GET
	Argument: Amount
	Return Value: ObiList<INoticeable>  
	Authorize: logged in 
	Possible Return Code Meaning: 200 = Success   
	
### Get User's Privacy State  
This API will get a user's Privacy State.  
[Source Code Of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FPrivacyApi.cs&_a=contents)  
[Definition of PrivacyStateViewModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FAccount%2FPrivacyStateViewModel.cs&_a=contents)  

	Address: /api/PrivacyState  
	Method: HTTP GET
	Argument: null  
	Return Value: ObiObject<PrivacyStateViewModel>  
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success
	
### Set User's Privacy State   
This API can set a user's privacy state.  
[Source Code Of this API](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FControllers%2FAPI%2FPrivacyApi.cs&_a=contents)  

	Address: /api/SetPrivacyState
	Method: HTTP POST
	Argument: null
	Post Content:  PrivacyStateViewModel
	Return Value: ServerReply
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success
	  
  
###Get All Posts  
This API can get a user's posts.  
[Definition of Posts](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FPosts.cs&_a=contents)  

	Address: /api/PostsCenter?Take=5
	Method: HTTP GET
	Argument: Take(int)
	Return Value: ObiList<Posts>  
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success
	
###Get posts in a group
This API can get a group's posts.  
** id is a group id **
[Definition of Posts](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FPosts.cs&_a=contents)    

	Address: /api/AllPostsFromGroup/5?Take=5
	Method: HTTP GET
	Argument: id(omitted),Take(int)
	Return Value: ObiList<Posts>   
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success
	
###Post a post in a group  
This API can post a post in a group.  
** id is a group id **   
** This API will return Post's value **

[Definition of PostsViewModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FBeautyTeam%2FPosts.cs&_a=contents)  

	Address: /api/PostAsAGroup/5
	Method: HTTP POST
	Argument: id(omitted)
	Post Content:  PostsViewModel
	Return Value: ObiValue<int>
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success
								  404 = Not Found Group
								  403 = Forbidden (means don't have permission)
						
								  
### /api/PostAsAMe

[Definition of PostsViewModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FBeautyTeam%2FPosts.cs&_a=contents)  

	Address: /api/PostAsAMe
	Method: HTTP POST
	Argument: null
	Post Content:  PostsViewModel
	Return Value: ObiValue<int>
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success

### Edit A Group's Post
This API can edit a group's post.  
[Definition of PostsViewModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FBeautyTeam%2FPosts.cs&_a=contents)  

	Address: /api/EditAGroupPost/5
	Method: HTTP POST
	Argument: id(omitted)
	Post Content:  PostsViewModel
	Return Value: ObiValue<int>
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success
								  404 = Not Found Post
								  403 = Forbidden (means don't have permission)
						
### Edit A Personal Post  
This API can edit a personal post.  
[Definition of PostsViewModel](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FViewModels%2FBeautyTeam%2FPosts.cs&_a=contents)  

	Address: /api/EditAPersonalPost/5
	Method: HTTP POST
	Argument: id(omitted)
	Post Content:  PostsViewModel
	Return Value: ObiValue<int>
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success
								  404 = Not Found Post
								  403 = Forbidden (means don't have permission)
								
### Add A Response to a post.  
This API can add a response to a post.  
** id is post's id **  
[Definition of PostResposneType](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FPosts.cs&_a=contents)  
	
	Address: /api/ResponseToPost/5
	Method: HTTP POST
	Argument: id(omitted)
	Post Content:  PostResposneType
	Return Value: ObiValue<int>
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success
								  403 = Forbidden (means don't have permission)
								  404 = Not Found Post
								  409 = Conflict (Already responsed)  
			
### Delete A Response In A Post.  
This API can delete a response in a post.  
** id is a post's id **

	Address: /api/UnResponseToPost/5
	Method: HTTP POST
	Argument: id(omitted)
	Post Content:  null
	Return Value: ObiValue<int>
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success
								  404 = Not Found Response
								  409 = Conflict (Not responsed)  
			
###CommetToPost
** id is a post's id **  

	Address: /api/CommetToPost/5
	Method: HTTP POST
	Argument: id(omitted)
	Post Content:  Content(string)
	Return Value: ObiValue<int>
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success
								  404 = Not Found Post

###CommetToCommet
** id is a FirCommentId **  
[Definition of FirComment](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FPosts.cs&_a=contents)      
[Definition of SecComment](http://source.obisoft.com.cn/tfs/DefaultCollection/BeautyTeam/_versionControl#path=%24%2FBeautyTeam%2FBeautyTeamSolution%2FBeautyTeamWeb%2FModels%2FPosts.cs&_a=contents)  
  
	Address: /api/CommetToCommet/5
	Method: HTTP POST
	Argument: id(omitted)
	Post Content:  Content(string)
	Return Value: ObiValue<int>
	Authorize: logged in
	Possible Return Code Meaning: 200 = Success
								  404 = Not Found FirComment
						
**Update this README.md file, God Yu!**.

