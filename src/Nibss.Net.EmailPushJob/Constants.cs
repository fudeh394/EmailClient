

namespace EmailPushJob
{
    public static class Constants
    {
        public const string USER_LOGIN_STATUS_ACTIVE = "A";
        public const string USER_LOGIN_STATUS_NOTACTIVE = "N";


        public const int ACCOUNT_TYPE_CHIT = 1;
        public const int ACCOUNT_TYPE_CARD = 2;





        public const int REQUEST_STATUS_SUCCESSFUL = 0;
        public const string REQUEST_STATUS_SUCCESSFUL_DESC = "Successful";
        public const int REQUEST_STATUS_FAILED = 1;
        public const string REQUEST_STATUS_FAILED_DESC = "Failed";
        public const int REQUEST_STATUS_PENDING = 2;
        public const string REQUEST_STATUS_PENDING_DESC = "Pending";


        public const int RESPONSE_STATUS_SUCCESSFUL = 0;
        public const string RESPONSE_STATUS_SUCCESSFUL_DESC = "Successful";
        public const int RESPONSE_STATUS_FAILED = 1;
        public const string RESPONSE_STATUS_FAILED_DESC = "Failed";
        public const int RESPONSE_STATUS_PENDING = 2;
        public const string RESPONSE_STATUS_PENDING_DESC = "Pending";
        public const int RESPONSE_STATUS_SENT = 3;
        public const string RESPONSE_STATUS_SENT_DESC = "SENT";




        public const string BVN_REQUESTSTATUS_APPROVED = "A";
        public const string BVN_REQUESTSTATUS_PENDING = "P";
        public const string BVN_REQUESTSTATUS_DECLINED = "D";
        public const string BVN_REQUESTSTATUS_COMMUNICATED = "C";
        public const string BVN_REQUESTSTATUS_FAILED = "F";

        public const int ROLE_TYPE_INPUTER = 1;
        public const int ROLE_TYPE_AUTHORISER = 2;
        public const int ROLE_TYPE_HEADOFFICE = 3;
        public const int ROLE_TYPE_ADMIN = 4;
    }
}
