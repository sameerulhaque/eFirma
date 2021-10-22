
(function ($) {
    "use strict";


    /*==================================================================
    [ Focus Contact2 ]*/
    $('.input100').each(function(){
        $(this).on('blur', function(){
            if($(this).val().trim() != "") {
                $(this).addClass('has-val');
            }
            else {
                $(this).removeClass('has-val');
            }
        })
    })


    /*==================================================================
    [ Validate ]*/
    var input = $('.validate-input .input100');

    $('.validate-form').on('submit',function(){
        var check = true;

        for(var i=0; i<input.length; i++) {
            if(validate(input[i]) == false){
                showValidate(input[i]);
                check=false;
            }
        }

        return check;
    });


    $('.validate-form .input100').each(function(){
        $(this).focus(function(){
           hideValidate(this);
        });
    });

    function validate (input) {
        if($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {
            if($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
                return false;
            }
        }
        else {
            if($(input).val().trim() == ''){
                return false;
            }
        }
    }

    function showValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).addClass('alert-validate');
    }

    function hideValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).removeClass('alert-validate');
    }


})(jQuery);

/*====================================Nav Bar===============================*/

//let selector = document.getElementById("selector");
let selector = document.querySelector('.selector');
let proPic = document.getElementById('proPic');

let proCopy = document.getElementById("proPicCopy");

$('#proPic').click(function (){
    console.log(proPic.src)
    proCopy.src = proPic.src
})

let element = document.querySelector('.modal-bg');

var ProfileModal = document.getElementById("profileModal");

var ProfileModelClose = document.getElementById("profile-model-close");

if (ProfileModelClose){
    ProfileModelClose.addEventListener('click',function () {
        ProfileModal.style.visibility= 'hidden';
        ProfileModal.style.opacity= '0';
    });
}
function ProfileModelVisible () {
    ProfileModal.style.visibility= 'visible';
    ProfileModal.style.opacity= '1';
}


function CloseModel(){
    ProfileModal.style.visibility= 'hidden';
    ProfileModal.style.opacity= '0';
}


/*====================================Home Page===============================*/
const dragArea = document.querySelector('.drag-area');
const  dragText = document.querySelector('.header');

let file;

if (dragArea){
    dragArea.addEventListener('dragover',(event) =>{
        event.preventDefault();
        console.log("Drag")
        dragText.textContent = 'Release to Upload';
        dragArea.classList.add('active')
    });
}
if (dragArea){
    dragArea.addEventListener('dragleave',(event) =>{
        console.log("Drag");
        dragText.textContent = 'Drag & Drop';
        dragArea.classList.remove('active')
    });
}

let Button = document.querySelector('.button');
let Input = document.querySelector('input');

if (Button){
    Button.onclick = () => {
        Input.click();
    }
}

Input.addEventListener('change',function (){
    file = this.files[0];
    dragArea.classList.add('active');
    displayFile();
})

var Modal = document.getElementById("homeModel");

var ModelClose = document.getElementById("model-close");

var pdfSection = document.querySelector('.pdfSection');

function ModelVisible () {
    Modal.style.visibility= 'visible';
    Modal.style.opacity= '1';
}
if (ModelClose){
    ModelClose.addEventListener('click',function () {
        Modal.style.visibility= 'hidden';
        Modal.style.opacity= '0';
    });
}

if (dragArea){
    dragArea.addEventListener('drop',(event) =>{
        event.preventDefault();
        console.log("Drop")
        file = event.dataTransfer.files[0];
        console.log(file);
        displayFile();
    });
}

let fileURL;

function displayFile(){
    let fileType = file.type;
    console.log(fileType)
    let validExtensions = ['application/pdf'];

    if (validExtensions.includes(fileType)){
        ModelVisible ();
        let fileReader = new FileReader();

        fileReader.onload = () => {
           fileURL = fileReader.result;
            console.log(fileURL);
            let pdfTag = `<iframe class="pdf" src="${fileURL}"></iframe>`;
            pdfSection.innerHTML = pdfTag;
        };
        fileReader.readAsDataURL(file);
    }else {
        alert("This file is an not PDF");
        dragArea.classList.remove('active')
    }
}
let ToggleBtn = document.getElementById('btnToggle');
let ToggleChekBox = document.getElementById('toggleCheckBox');

function chekBox(){
    console.log(ToggleChekBox.val());
}

$("#btnToggle").click(function (){
    if (ToggleChekBox.checked!=true){
        console.log("True")
    }else {
        console.log("false")
    }
});

let btnEveryBody = document.getElementById('everyBody');
let btnInProgress = document.getElementById('inProgress');
let btnSigned = document.getElementById('signed');
let btnArchived = document.getElementById('archived');

let EveryBodySection = document.getElementById('everyBodySection');
if (EveryBodySection){
    EveryBodySection.style.display= 'block';
    btnEveryBody.style.color='#EE332D';
}

let InProgressSection = document.getElementById('inProgressSection');
if (InProgressSection){
    InProgressSection.style.display= 'none';

}

let signedSection = document.getElementById('signedSection');
if (signedSection){
    signedSection.style.display= 'none';
}

let ArchivedSection = document.getElementById('archivedSection');
if (ArchivedSection){
    ArchivedSection.style.display= 'none';
}

if (btnEveryBody){
    btnEveryBody.addEventListener('click',function (){
        EveryBodySection.style.display= 'block';
        InProgressSection.style.display= 'none';
        signedSection.style.display= 'none';
        ArchivedSection.style.display= 'none';
        btnEveryBody.style.color='#EE332D';
        btnArchived.style.color='black';
        btnSigned.style.color='black';
        btnInProgress.style.color='black';
    });

}

if (btnInProgress){
    btnInProgress.addEventListener('click',function (){
        EveryBodySection.style.display= 'none';
        InProgressSection.style.display= 'block';
        signedSection.style.display= 'none';
        ArchivedSection.style.display= 'none';
        btnEveryBody.style.color='black';
        btnInProgress.style.color='#EE332D';
        btnSigned.style.color='black';
        btnArchived.style.color='black';
    });
}


if (btnSigned){
    btnSigned.addEventListener('click',function (){
        EveryBodySection.style.display= 'none';
        InProgressSection.style.display= 'none';
        signedSection.style.display= 'block';
        ArchivedSection.style.display= 'none';
        btnEveryBody.style.color='black';
        btnSigned.style.color='#EE332D';
        btnInProgress.style.color='black';
        btnArchived.style.color='black';
    });
}


if (btnArchived){
    btnArchived.addEventListener('click',function (){
        EveryBodySection.style.display= 'none';
        InProgressSection.style.display= 'none';
        signedSection.style.display= 'none';
        ArchivedSection.style.display= 'block';
        btnEveryBody.style.color='black';
        btnArchived.style.color='#EE332D';
        btnSigned.style.color='black';
        btnInProgress.style.color='black';
    })
}


$('#btnManage').click(function (){
    console.log("Manage");
});

/*=====================================document Page====================================*/

function menuToggle(){
    let toggleMenu = document.querySelector('.profileMenu');
    toggleMenu.classList.toggle('active')
}


let btnPreparation = document.getElementById("btnPreparation");
let btnDocument = document.getElementById("btnDocument");

let IFrame = document.getElementById('iFrame');

let preparationSection = document.getElementById("preparationSection");
if (preparationSection){
    preparationSection.style.display='block';
    btnPreparation.style.backgroundColor='#2e3133';
    btnPreparation.style.color='white';
    btnDocument.style.color='black';
}
let documentSection = document.getElementById("documentSection");
if (documentSection){
    documentSection.style.display='none'
}
if (btnPreparation){
    btnPreparation.addEventListener('click',function (){
        preparationSection.style.display='block'
        documentSection.style.display='none'
        btnPreparation.style.backgroundColor='#2e3133';
        btnPreparation.style.color='white';
        btnDocument.style.backgroundColor='white';
        btnDocument.style.color='black';
    })
}

if (btnDocument){
    btnDocument.addEventListener('click',function (){
        preparationSection.style.display='none'
        documentSection.style.display='block'
        btnPreparation.style.backgroundColor='white';
        btnPreparation.style.color='black';
        btnDocument.style.color='white';
        btnDocument.style.backgroundColor='#2e3133';
        IFrame.src= fileURL;

    })
}

function create_tr(table_id) {
    let table_body = document.getElementById(table_id),
        first_tr   = table_body.firstElementChild
    tr_clone   = first_tr.cloneNode(true);

    table_body.append(tr_clone);

    clean_first_tr(table_body.firstElementChild);
}

function clean_first_tr(firstTr) {
    let children = firstTr.children;

    children = Array.isArray(children) ? children : Object.values(children);
    children.forEach(x=>{
        if(x !== firstTr.lastElementChild)
        {
            x.firstElementChild.value = '';
        }
    });
}
function remove_tr(This) {
    if(This.closest('tbody').childElementCount == 1)
    {
        alert("You Don't have Permission to Delete This ?");
    }else{
        This.closest('tr').remove();
    }
}

let Plus = document.getElementById('plus');
if (Plus){
    Plus.style.display='none'
}

$('#addViewer').click(function () {
    Plus.style.display='block'
})

function create_viewers_tr(table_id) {
    let table_body = document.getElementById(table_id),
        first_tr   = table_body.firstElementChild
    tr_clone   = first_tr.cloneNode(true);

    table_body.append(tr_clone);

    clean_first_tr(table_body.firstElementChild);
}

let ToggleMessageBtn = document.getElementById('btnMessageToggle');
let ToggleMessageChekBox = document.getElementById('toggleMessageCheckBox');
let MSGBox = document.getElementById('msgBoxSection');

if (MSGBox){
    MSGBox.style.display="none"
}

$("#btnMessageToggle").click(function (){
    if (ToggleMessageChekBox.checked!=true){
        MSGBox.style.display='block'
    }else {
        MSGBox.style.display='none'
    }
});


var PaymentModal = document.getElementById("PaymentModal");

var PaymentModelClose = document.getElementById("payment-model-close");



function PaymentModelVisible () {
    PaymentModal.style.visibility= 'visible';
    PaymentModal.style.opacity= '1';
}
if (PaymentModelClose){
    PaymentModelClose.addEventListener('click',function () {
        PaymentModal.style.visibility= 'hidden';
        PaymentModal.style.opacity= '0';
    });
}


$("#requestSignatures").click(function (e) {
    e.preventDefault();
    PaymentModelVisible();
})


