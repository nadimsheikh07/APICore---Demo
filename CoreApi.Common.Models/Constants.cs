using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.Common.Models
{
    public class Constants
    {
        public const string DUPLICTE_PROJECT_NAME = "Project name is already exist! Please enter different project name.";
        public const string DUPLICTE_PROJECT_NUMBER = "Project number (ebiz) is already exist! Please enter different Project number (ebiz).";
        public const string DUPLICTE_PROJECT_SPV = "Spv is already exist! Please enter different spv.";
        public const string DUPLICTE_SERVICE_NAME = "Service name is already exist! Please enter different service name.";
        public const string DUPLICTE_SERVICE_CODE = "Service short code is already exist! Please enter different service short code.";
        public const string DUPLICTE_GAA_NAME = "Gaa name is already exist! Please enter different gaa name.";
        public const string BLANK_PROJECT_NUMBER = "Please enter project number (ebiz).";
        public const string BLANK_PURCHASE_ORDER_NUMBER = "Please enter purchase order number.";
        public const string BLANK_PURCHASE_ORDER_DATE = "Please enter Purchase order date.";
        public const string BLANK_CUSTOMER_NAME = "Please enter customer name.";
        public const string BLANK_PROJECT_NAME = "Please enter project name.";
        public const string BLANK_DESCRIPTION = "Please enter description.";
        public const string BLANK_START_DATE = "Please enter start date.";
        public const string BLANK_END_DATE = "Please enter end date.";
        public const string BLANK_PERMITTED_CUSTOMER_ID_FOR_KDF_UPLOAD = "Please enter permitted customer id(s) (for kdf upload).";
        public const string BLANK_SPV = "Please enter spv.";
        public const string BLANK_SERVICE_NAME = "Please enter service name.";
        public const string BLANK_SERVICE_SHORT_CODE = "Please enter service short code.";
        public const string BLANK_SERVICE_PRIORITY = "Please select priority.";
        public const string BLANK_RUN_AS_STAND_ALONE = "Please enter run as stand alone.";
        public const string BLANK_ACTIVITY_NAME = "Please enter activity name.";
        public const string BLANK_FROM_SANCTIONED_LOAD = "Please enter from sanctioned load(kw).";
        public const string BLANK_TO_SANCTIONED_LOAD = "Please enter To sanctioned load(kw).";
        public const string BLANK_GAA_NAME = "Please enter gaa name.";
        public const string BLANK_CONFIGURATION_TYPE = "Please select configuration type.";
        public const string BLANK_SERVICE = "Please select service.";
        public const string BLANK_JOB_TYPE = "Please select Job type."; 
        public const string BLANK_ALLOW_ACTIVITY_TYPE = "Please enter allow activity type."; 
        public const string BLANK_CATEGORY = "Please select category.";
        public const string BLANK_ITEM = "Please select item.";
        public const string BLANK_ACTIVITY = "Please select activity.";
        public const string BLANK_LOCATION = "Please select location.";
        public const string BLANK_MANDATORY = "Please select mandatory.";
        public const string BLANK_FROM_INVENTORY = "Please select from inventory.";
        public const string BLANK_MULTIPLE_ENTRY = "Please select multiple entry.";
        public const string BLANK_DEFAULT_QUANTITY = "Please select default quantity per item.";
        public const string BLANK_MIN_QUANTITY = "Please select min quantity per item.";
        public const string BLANK_MAX_QUANTITY = "Please select max quantity per item.";
        public const string BLANK_COMPONENT = "Please select component.";
        public const string BLANK_JOB_TYPE_CODE = "Please enter job type code.";
        public const string BLANK_DATE_OF_EFFECT = "Please select date of effect.";
        public const string BLANK_STOCK_CATEGORY = "Please select stock category.";
        public const string BLANK_ITEM_NAME = "Please select item name.";
        public const string BLANK_QUANTITY = "Please enter quantity.";
        public const string MIN_MAX_ERROR = "Maximum value should be larger than minimum value.";
        public const string BLANK_DEFAULT_CONSUME = "Please select default consume.";
        public const string BLANK_GAA_LEVEL = "Please enter gaa level.";
        public const string BLANK_GAA_CODE = "Please enter gaa code.";
        public const string BLANK_PARENT_GAA = "Please enter parent gaa.";
        public const string BLANK_SYSTEM_LEVEL_ACTIVITY = "Please select issystem level.";
        public const string GREATER_FROM_TO_SANCTIONED_LOAD = "To sanctioned load should be greater from sanctioned load.";
        public const string START_DATE_VALIDATION = "Start date should be less than end date.";
        public const string useralreadylogged = "User already logged in.";
        public const string unAuthorizeuser = "Unauthorize user. Please login again.";
        public const string BLANK_API_URL = "Please enter api url.";
        public const string BLANK_SUPPLIER_CODE = "Please enter supplier code.";
        public const string BLANK_USER_NAME = "Please enter user name.";
        public const string BLANK_PS = "Please enter password.";
        public const string DUPLICTE_ACTIVITY_NAME = "Activity name already exist! Please select different activity name.";
        public const string Invalid_Token = "Invalid token";
        public const string Invaliduserid = "Invalid user id";
        public const string Invaliddatecriteria = "Invalid date criteria";
        public const string Invalidproductid = "Invalid product id";
        public const string Invalidclientid = "Invalid client id";
        public const string Noinputfile = "No input file";
        public const string Invalidrequest = "Invalid request";
        public const string Itemkitforselectedjobisnotavailable = "Item kit for selected job(s) is not available.";
        public const string Notrackingfoundforsearcheditem = "No tracking found for searched item. Please check the serial number and try again.";
        public const string StringRegularExpression = @"^[a-zA-Z0-9.!\s@?#$%&:'"";()*\+,\/;\-=[\\\]\^_{|}<>~` ]+$";
        public const string BLANK_CONSUMER_NAME = "Consumer name is mandatory.";
        public const string BLANK_CONSUMER_NUMBER = "Consumer number is mandatory.";
        public const string BLANK_CONSUMER_ADDRESS = "Consumer address is mandatory.";
        public const string BLANK_CONSUMER_MOBILE = "Consumer mobile number is mandatory.";
        public const string WRONG_CONSUMER_METERTYPE = "Invalid value of consumer meter type.";
        public const string WRONG_FUELTYPE = "Invalid value of fuel type.";
        public const string BLANK_CONSUMER_SERIALNUMBER = "Consumer meter serial number is mandatory.";
        public const string BLANK_CONSUMER_METERBLE = "Consumer's meter is BLE or not field is mandatory.";
        public const string CONSUMER_DATA_CHECK = "Consumer request data is mandatory.";
        public const string BLANK_GAA_DATA_NAME = "Gaa name is mandatory.";
        public const string BLANK_GAA_DATA_LEVEL_ID = "Gaa level id is mandatory.";
        public const string BLANK_SOURCE_GAA_ID = "Source gaa id is mandatory.";
        public const string GAA_DATA_CHECK = "Gaa request data is mandatory.";

        #region Job Management

        public const string EMPTY_MANDATORY_FIELD_JRD = "JobRequestDetails's mandatory field received NULL/empty value";
        public const string EMPTY_MANDATORY_FIELD_JWRP = "JobWiseRequestParam's mandatory field received NULL/empty value";
        public const string UNDEFINED_ENUM = "Field not in pre-defined value range";
        public const string VALUE_OVERFLOW = "Field's Value exceeds expected size";
        public const string INVALID_JOBWISEREQUESTPARAMS = "Invalid format of JobWiseRequestParams for Job type";
        public const string DATABASE_EXCEPTION = "Error occured on database side";
        public const string INVALID_ID_OR_SPVCODE = "No Job had been created with respect to the given Id or SPVCode";
        public const string EMPTY_MANDATORY_FIELD_ODJR = "OnDemandJobRequest's mandatory field received NULL/empty value";
        public const string INTERNAL_SERVER_ERROR = "Internl server error";

        #endregion

        #region Project Management

        public const string EMPTY_MANDATORY_FIELD_GLR = "GAALevelRequest's mandatory field received NULL/empty value";
        public const string EMPTY_MANDATORY_FIELD_GLIR = "GAALevelIdRequest's mandatory field received NULL/empty value";
        public const string EMPTY_MANDATORY_FIELD_GLNR = "GAALevelNameRequest's mandatory field received NULL/empty value";

        #endregion
    }
}
