General Notes
*************

This was an assignment that I completed over a weekend with minimal knowledge of ASP MVC 3. I have more experience with server side Javascript / Go(lang) and Linux / Nginx stack so there will be parts where it is incorrect or not optimal. 

I decided to push this code up to show what I accomplished with the lack of experience that I have. 

Some decisions I made and why follows
* Straight ADO.Net - I am not a huge fan of ORM's in general. They are an anti-pattern. Great for initial support of multiple DBMS but later they end up hurting in terms of performance, bugs and so on. If I had to use one I would be using something lightweight like https://github.com/StackExchange/dapper-dot-net  not something heavy like the Entity framework.

* Usage of Linq - This is superfluous in this assignment. The data layer should be filtering out the data at sql level and not passing it back to the controller e.g PatientVisits in Patient Controller. This was done to exlempify that I have some experience with Linq.

Please excuse some of the poor commit messages, next time I will rebase and squashe them up. 

Milestone 1 Notes
*******************

Please run schema.sql which resides in Hospital/database to create all the schema.

To login please use one of the following user names
mary, fred or brad

the password is
username (with a capital) followed by Secret123!

The reasoning behind the password is because it involves all of the keyspaces and that makes it more secure :-)

Milestone 2 Notes
****************

DoctorPatientInfo
   * Assign a doctor to a patient -> Go to Doctors Menu up the top and click Assign Patient
   * Treatment History of each doctor (i.e which patient he/she has treated in past and/or is treating) -> Go to Doctors Menu up the top and click Patient History

DischargeInPatient
   * A Page that calculates amount payable by the "in-patient" upon discharge. -> Go To Visits menu at the top. Find a patient that is type in-patient - Click the Discharge button on the right. Observe that it shows the length of stay, rate per day and a subtotal (presented as an invoice ready to pay

   * Presenting of an Invoice, payment process and change of status (patient -> dischared) in the database- Same as above. Stat to type in credit card numbers (e.g sample card numbers are on http://www.paypalobjects.com/en_US/vhelp/paypalmanager_help/credit_card_numbers.htm  , enter expiry , name and click the top right to turn card over to fill out security number. Observe that it will not allow you to process payment until Luhn check etc is completed. Once correct you can click Process payment.. click it.. payment completes and it goes back to PAtient Vists list. Observe that patient is no longer an in-patient and they have a discharge date.


According to the Forum it is possible to write or use an existing plugin for cc validation and I did so.

Attribution goes to https://github.com/kenkeiter/skeuocard
