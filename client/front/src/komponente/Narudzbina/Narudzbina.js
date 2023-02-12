import React from 'react';
import './Narudzbina.css';
import { Modal, Form, FormControl, Button, InputGroup, DropdownButton, Dropdown } from 'react-bootstrap';
import axios from 'axios';
import { useState, useEffect } from "react";
import { useParams } from 'react-router-dom';

const Narudzbina = (props) => {
    const { show, onHide, CarID, } = props;

    const [OccupiedFrom, setOccupiedFrom] = useState('');
    const [OccupiedUntil, setOccupiedUntil] = useState('');

    const [dealer, setDealer] = useState('');



    let test = localStorage.getItem('user-info');
    let UserID = null;
    if (test) {
        test = JSON.parse(test);
        UserID = test.id;


    }

    useEffect(() => {

        axios.get("https://localhost:44341/Dealer/GetAllDealers")
            .then(res => {
                console.log(res)
                setDealer(res.data)
            })

            .catch(err => {
                console.log(err)
            })
    }, [])

    console.log(dealer);


    // async function order(OccupiedFrom,OccupiedUntil, dealer, UserID) {
    //     console.log(OccupiedFrom, OccupiedUntil, dealer, UserID);


    //     if ( !OccupiedFrom || !OccupiedUntil || !Dea || !CarID || !UserID ) {
    //         console.error("Nedostaju neke od obaveznih varijabli");
    //         return;
    //     }

    //     try {
    //         const response = await axios.post(`https://localhost:44341//RentCar/MakeCarRental/${OccupiedFrom}/${OccupiedFrom}/${CarID}/${DealerID}/${UserID}`);


    //         if (response.status !== 200) {
    //             console.error(`API odgovor nije uspeo: ${response.status}`);
    //             return;
    //         }

    //         const data = response.data;



    //         alert("Narudzbina je poslata dealeru");
    //         console.log(response);
    //     } catch (error) {
    //         console.error(error);
    //     }
    // }







    return (
        <Modal {...props} size="lg" centered>
            <Modal.Header closeButton>
                <Modal.Title><strong>{props.name}</strong></Modal.Title>
            </Modal.Header>
            <Modal.Body>

                <Form>

                    <Form.Group>
                        <Form.Label><strong>Popunite </strong></Form.Label>
                    </Form.Group>
                    <Form.Group>
                        <Form.Label><strong>OccupiedFrom</strong></Form.Label>
                        <Form.Control type="date" value={OccupiedFrom} onChange={e => setOccupiedFrom(e.target.value)} />
                    </Form.Group>
                    <Form.Group>
                        <Form.Label><strong>OccupiedUntil</strong></Form.Label>
                        <Form.Control type="date" value={OccupiedUntil} onChange={e => setOccupiedUntil(e.target.value)} />
                    </Form.Group>
                    <Form.Group>
                        <Form.Label><strong>Dealer</strong></Form.Label>
                        <Form.Control as="select" value={dealer} onChange={e => setDealer(e.target.value)}>
                            <option value="">dealer</option>
                            {dealer && dealer.length > 0 && dealer.map(d => (
                                <option key={d.id} value={d.name}>{d.name}</option>
                            ))}
                        </Form.Control>
                    </Form.Group>



                    {/* <Button onClick={() => order(OccupiedFrom, OccupiedUntil, dealer, UserID)}>Rezervisi</Button> */}

                </Form>
            </Modal.Body>
        </Modal>
    )
}
export default Narudzbina;