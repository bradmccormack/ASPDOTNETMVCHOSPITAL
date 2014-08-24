Milestone 1 Notes
*******************

Please run createall.sql which resides in Hospital/database to create all the schema.

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
Matthew Bolger  INSTRUCTOR MANAGER  
RE: Credit card validation requirementRE: Credit card validation requirement
COLLAPSE
View Original Post Parent Post
You can use a plugin or write your own. Note that jQuery itself already has credit card validation code ready to be used - you just need to hook into it.

We expect a proper credit card check - so its more than just 16 digits. There are alogirthms that can be used against these various card types which extends beyond the starting digit although that forms part of the check.

Attribution goes to https://github.com/kenkeiter/skeuocard
