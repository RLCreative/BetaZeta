/*!
 * Copyright 2012, William Jarvela
 */
function divChange() 
{
    if (document.getElementById('CategoryID').value == 0) 
    {
        //document.getElementById('element_id').style.visibility = 'visible';
    }
    else if (document.getElementById('CategoryID').value == 1) 
    {
        document.getElementById('RegHeader').style.display = '';
        document.getElementById('RegList').style.display = 'none';
        document.getElementById('Cybex').style.display = '';
        document.getElementById('CybexCar').style.display = 'none';
        document.getElementById('Lascal').style.display = 'none';
        document.getElementById('ContactInfo').style.display = '';
    }
    else if (document.getElementById('CategoryID').value == 2) 
    {            
        document.getElementById('RegHeader').style.display = 'none';
        document.getElementById('RegList').style.display = 'none';
        document.getElementById('Cybex').style.display = 'none';
        document.getElementById('CybexCar').style.display = '';
        document.getElementById('Lascal').style.display = 'none';
        document.getElementById('ContactInfo').style.display = '';
    }
    else if (document.getElementById('CategoryID').value == 3) 
    {
        document.getElementById('RegHeader').style.display = '';
        document.getElementById('RegList').style.display = 'none';
        document.getElementById('Cybex').style.display = 'none';
        document.getElementById('CybexCar').style.display = 'none';
        document.getElementById('Lascal').style.display = '';
        document.getElementById('ContactInfo').style.display = '';
    }
        
};

  
    function updateState() 
    {
        document.getElementById('State').value = document.getElementById('ddlState').value;
    };
        
    function updateCountry() 
    {    
        document.getElementById('Country').value = document.getElementById('ddlCountry').value;
    };

    function Validate() {


        if (document.getElementById('CategoryID').value == 0) {
            //document.getElementById('element_id').style.visibility = 'visible';
        }
        else if (document.getElementById('CategoryID').value == 1) {
            if ((document.getElementById('ModName').value == '') || (document.getElementById('modelNum').value == '') || (document.getElementById('manDate').value == '')) {
                if ((document.getElementById('ModName').value == '') && (document.getElementById('modelNum').value == '') && (document.getElementById('manDate').value == '')) {
                    document.getElementById('RequiredMessage0').style.display = '';
                    document.getElementById('RequiredMessage1').style.display = '';
                    document.getElementById('RequiredMessage2').style.display = '';
                    return false;
                }
                else if ((document.getElementById('ModName').value != '') && (document.getElementById('modelNum').value == '') && (document.getElementById('manDate').value == '')) {
                    document.getElementById('RequiredMessage0').style.display = 'none';
                    document.getElementById('RequiredMessage1').style.display = '';
                    document.getElementById('RequiredMessage2').style.display = '';
                    return false;
                }
                else if ((document.getElementById('ModName').value == '') && (document.getElementById('modelNum').value != '') && (document.getElementById('manDate').value == '')) {
                    document.getElementById('RequiredMessage0').style.display = '';
                    document.getElementById('RequiredMessage1').style.display = 'none';
                    document.getElementById('RequiredMessage2').style.display = '';
                    return false;
                }
                else if ((document.getElementById('ModName').value == '') && (document.getElementById('modelNum').value == '') && (document.getElementById('manDate').value != '')) {
                    document.getElementById('RequiredMessage0').style.display = '';
                    document.getElementById('RequiredMessage1').style.display = '';
                    document.getElementById('RequiredMessage2').style.display = 'none';
                    return false;
                }
                else if ((document.getElementById('ModName').value != '') && (document.getElementById('modelNum').value != '') && (document.getElementById('manDate').value == '')) {
                    document.getElementById('RequiredMessage0').style.display = 'none';
                    document.getElementById('RequiredMessage1').style.display = 'none';
                    document.getElementById('RequiredMessage2').style.display = '';
                    return false;
                }
                else if ((document.getElementById('ModName').value == '') && (document.getElementById('modelNum').value != '') && (document.getElementById('manDate').value != '')) {
                    document.getElementById('RequiredMessage0').style.display = '';
                    document.getElementById('RequiredMessage1').style.display = 'none';
                    document.getElementById('RequiredMessage2').style.display = 'none';
                    return false;
                }
                else if ((document.getElementById('ModName').value != '') && (document.getElementById('modelNum').value == '') && (document.getElementById('manDate').value != '')) {
                    document.getElementById('RequiredMessage0').style.display = 'none';
                    document.getElementById('RequiredMessage1').style.display = '';
                    document.getElementById('RequiredMessage2').style.display = 'none';
                    return false;
                }
                else if ((document.getElementById('ModName').value != '') && (document.getElementById('modelNum').value != '') && (document.getElementById('manDate').value != '')) {
                    document.getElementById('RequiredMessage0').style.display = 'none';
                    document.getElementById('RequiredMessage1').style.display = 'none';
                    document.getElementById('RequiredMessage2').style.display = 'none';
                    //return true;
                    ValidateStateCountry();
                }
            }
            else {
                document.getElementById('RequiredMessage0').style.display = 'none';
                document.getElementById('RequiredMessage1').style.display = 'none';
                document.getElementById('RequiredMessage2').style.display = 'none';
                //return true;
                ValidateStateCountry();
            }
        }
            /////Cybex Car Seats
        else if (document.getElementById('CategoryID').value == 2) {
            if ((document.getElementById('ModNameCarSeat').value == '') || (document.getElementById('ModNumCarSeat').value == '') || (document.getElementById('ManDateCarSeat').value == '')) {
                if ((document.getElementById('ModNameCarSeat').value == '') && (document.getElementById('ModNumCarSeat').value == '') && (document.getElementById('ManDateCarSeat').value == '')) {
                    document.getElementById('RequiredMessage3').style.display = '';
                    document.getElementById('RequiredMessage4').style.display = '';
                    document.getElementById('RequiredMessage5').style.display = '';
                    return false;
                }
                else if ((document.getElementById('ModNameCarSeat').value != '') && (document.getElementById('ModNumCarSeat').value == '') && (document.getElementById('ManDateCarSeat').value == '')) {
                    document.getElementById('RequiredMessage3').style.display = 'none';
                    document.getElementById('RequiredMessage4').style.display = '';
                    document.getElementById('RequiredMessage5').style.display = '';
                    return false;
                }
                else if ((document.getElementById('ModNameCarSeat').value == '') && (document.getElementById('ModNumCarSeat').value != '') && (document.getElementById('ManDateCarSeat').value == '')) {
                    document.getElementById('RequiredMessage3').style.display = '';
                    document.getElementById('RequiredMessage4').style.display = 'none';
                    document.getElementById('RequiredMessage5').style.display = '';
                    return false;
                }
                else if ((document.getElementById('ModNameCarSeat').value == '') && (document.getElementById('ModNumCarSeat').value == '') && (document.getElementById('ManDateCarSeat').value != '')) {
                    document.getElementById('RequiredMessage3').style.display = '';
                    document.getElementById('RequiredMessage4').style.display = '';
                    document.getElementById('RequiredMessage5').style.display = 'none';
                    return false;
                }
                else if ((document.getElementById('ModNameCarSeat').value != '') && (document.getElementById('ModNumCarSeat').value != '') && (document.getElementById('ManDateCarSeat').value == '')) {
                    document.getElementById('RequiredMessage3').style.display = 'none';
                    document.getElementById('RequiredMessage4').style.display = 'none';
                    document.getElementById('RequiredMessage5').style.display = '';
                    return false;
                }
                else if ((document.getElementById('ModNameCarSeat').value == '') && (document.getElementById('ModNumCarSeat').value != '') && (document.getElementById('ManDateCarSeat').value != '')) {
                    document.getElementById('RequiredMessage3').style.display = '';
                    document.getElementById('RequiredMessage4').style.display = 'none';
                    document.getElementById('RequiredMessage5').style.display = 'none';
                    return false;
                }
                else if ((document.getElementById('ModNameCarSeat').value != '') && (document.getElementById('ModNumCarSeat').value == '') && (document.getElementById('ManDateCarSeat').value != '')) {
                    document.getElementById('RequiredMessage3').style.display = 'none';
                    document.getElementById('RequiredMessage4').style.display = '';
                    document.getElementById('RequiredMessage5').style.display = 'none';
                    return false;
                }
                else if ((document.getElementById('ModNameCarSeat').value != '') && (document.getElementById('ModNumCarSeat').value != '') && (document.getElementById('ManDateCarSeat').value != '')) {
                    document.getElementById('RequiredMessage3').style.display = 'none';
                    document.getElementById('RequiredMessage4').style.display = 'none';
                    document.getElementById('RequiredMessage5').style.display = 'none';
                    //return true;
                    ValidateStateCountry();
                }
            }
            else {
                document.getElementById('RequiredMessage3').style.display = 'none';
                document.getElementById('RequiredMessage4').style.display = 'none';
                document.getElementById('RequiredMessage5').style.display = 'none';
                //return true;
                ValidateStateCountry();
            }
        }
            //    /////Lascal
        else if (document.getElementById('CategoryID').value == 3) {
            if ((document.getElementById('ModNameLascal').value == '') || (document.getElementById('ModNumLascal').value == '') || (document.getElementById('ManDateLascal').value == '')) {
                if ((document.getElementById('ModNameLascal').value == '') && (document.getElementById('ModNumLascal').value == '') && (document.getElementById('ManDateLascal').value == '')) {
                    document.getElementById('RequiredMessage6').style.display = '';
                    document.getElementById('RequiredMessage7').style.display = '';
                    document.getElementById('RequiredMessage8').style.display = '';
                    return false;
                }
                else if ((document.getElementById('ModNameLascal').value != '') && (document.getElementById('ModNumLascal').value == '') && (document.getElementById('ManDateLascal').value == '')) {
                    document.getElementById('RequiredMessage6').style.display = 'none';
                    document.getElementById('RequiredMessage7').style.display = '';
                    document.getElementById('RequiredMessage8').style.display = '';
                    return false;
                }
                else if ((document.getElementById('ModNameLascal').value == '') && (document.getElementById('ModNumLascal').value != '') && (document.getElementById('ManDateLascal').value == '')) {
                    document.getElementById('RequiredMessage6').style.display = '';
                    document.getElementById('RequiredMessage7').style.display = 'none';
                    document.getElementById('RequiredMessage8').style.display = '';
                    return false;
                }
                else if ((document.getElementById('ModNameLascal').value == '') && (document.getElementById('ModNumLascal').value == '') && (document.getElementById('ManDateLascal').value != '')) {
                    document.getElementById('RequiredMessage6').style.display = '';
                    document.getElementById('RequiredMessage7').style.display = '';
                    document.getElementById('RequiredMessage8').style.display = 'none';
                    return false;
                }
                else if ((document.getElementById('ModNameLascal').value != '') && (document.getElementById('ModNumLascal').value != '') && (document.getElementById('ManDateLascal').value == '')) {
                    document.getElementById('RequiredMessage6').style.display = 'none';
                    document.getElementById('RequiredMessage7').style.display = 'none';
                    document.getElementById('RequiredMessage8').style.display = '';
                    return false;
                }
                else if ((document.getElementById('ModNameLascal').value == '') && (document.getElementById('ModNumLascal').value != '') && (document.getElementById('ManDateLascal').value != '')) {
                    document.getElementById('RequiredMessage6').style.display = '';
                    document.getElementById('RequiredMessage7').style.display = 'none';
                    document.getElementById('RequiredMessage8').style.display = 'none';
                    return false;
                }
                else if ((document.getElementById('ModNameLascal').value != '') && (document.getElementById('ModNumLascal').value == '') && (document.getElementById('ManDateLascal').value != '')) {
                    document.getElementById('RequiredMessage6').style.display = 'none';
                    document.getElementById('RequiredMessage7').style.display = '';
                    document.getElementById('RequiredMessage8').style.display = 'none';
                    return false;
                }
                else if ((document.getElementById('ModNameLascal').value != '') && (document.getElementById('ModNumLascal').value != '') && (document.getElementById('ManDateLascal').value != '')) {
                    document.getElementById('RequiredMessage6').style.display = 'none';
                    document.getElementById('RequiredMessage7').style.display = 'none';
                    document.getElementById('RequiredMessage8').style.display = 'none';
                    //ValidateStateCountry();
                    //return true;
                    ValidateStateCountry();
                }
            }
            else {
                document.getElementById('RequiredMessage6').style.display = 'none';
                document.getElementById('RequiredMessage7').style.display = 'none';
                document.getElementById('RequiredMessage8').style.display = 'none';
                //ValidateStateCountry();
                //return true;
                ValidateStateCountry();
            }
        }
    };
    