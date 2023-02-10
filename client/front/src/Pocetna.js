import React, { useEffect, useState } from 'react';
import PreporuceniProizvodi from './komponente/PreporuceniProizvodi/PreporuceniProizvodi';
import axios from "axios";

import {
  MDBContainer,
  MDBRow,
  MDBCol,
  MDBCard,
  MDBCardBody,
  MDBCardImage,
  MDBIcon,
  MDBRipple,
  MDBBtn,
  MDBDropdownToggle,
  MDBDropdownMenu,
  MDBDropdownItem,
  MDBDropdown
} from "mdb-react-ui-kit";




function Pocetna() {

    const [marks, setMarks] = useState();
    const [models, setModels] = useState(false);
    const [price, setPrice] = useState(false);
    const [fuelTypes, setFuelTypes] = useState(false);
    const [mark, setMark] = useState(false);

    function changeMark(m)
    {
      setMark(m);
    }


    useEffect(()=>{
   
    
      // console.log(user);
      // if (user!=null)
      //  setUserinfo(user);
      // console.log(user_info);
      //console.log(del);
  
      
  
  
      axios.get("https://localhost:44341/Car/GetMarka")
      .then(res => {
        console.log(res)
        setMarks(res.data)
      })
      .catch(err => {
        console.log(err)
      })
      
      console.log(marks);

      if (mark != false) {
        
       console.log(mark);
        axios.get("https://localhost:44341/Car/GetModelsFromMark/"+mark.id)
        .then(res => {
          //console.log(res)
          setModels(res.data)
          
        })
        .catch(err => {
          console.log(err)
        })

        console.log(models);
      }
      
  
      
    },[mark])

    
    
    // useEffect(()=>{
      

    // },[mark.id])

    
    

    
    return (
        <>
      <div style={{ textAlign: 'center', fontWeight: 'bold', fontSize: 'large' }}>
       Pogledajte proizvode preporucene od strane drugih korisnika
      </div>
     
      <MDBContainer fluid>
       <MDBRow className="justify-content-center mb-0">
        <MDBCol md="12" xl="10">
          <MDBCard className="shadow-0 border rounded-3 mt-5 mb-3">
            <MDBCardBody className='justify-content-row'>
            <div style={{ textAlign: 'center', fontWeight: 'bold', fontSize: 'large' }}>
                Pronadjite zeljeni automobil
              </div>

              <MDBDropdown>
                <MDBDropdownToggle tag='a' className='nav-link' role='button'>
                  Marka
                </MDBDropdownToggle>
                <MDBDropdownMenu>
                  {marks ? marks.map( m => (
                     <MDBDropdownItem key={m.id}  onClick={() => changeMark(m)}>  
                     {m.name}
                   </MDBDropdownItem>

                  )): <div>Loading...</div>}
                </MDBDropdownMenu>
              </MDBDropdown>


              <MDBDropdown className='justify-contetnt-center'>
                <MDBDropdownToggle tag='a' className='nav-link' role='button'>
                  Model
                </MDBDropdownToggle>
                <MDBDropdownMenu>
                  {models ? models.map( m => (
                     <MDBDropdownItem key={m.id}  >  
                     {m.name}
                   </MDBDropdownItem>

                  )): <div>Loading...</div>}
                </MDBDropdownMenu>
              </MDBDropdown>

            </MDBCardBody>
          </MDBCard>
        </MDBCol>
       </MDBRow>
      </MDBContainer>
     
      <div>
      <PreporuceniProizvodi />
      </div>
      </>
    );
  }
  
  export default Pocetna;
  