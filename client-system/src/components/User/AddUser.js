import style from '../../theme/style'
import React, { useState } from 'react';
import { Button, Container, Grid, TextField, Typography } from '@mui/material';
import { post } from '../../Services/HttpClient';

const AddUser = () => {
    const [user, setUser] = useState({
        Email: '',
        FirstName: '',
        LastName: '',
        Document: '',
        Address: '',
        Password: '',
        PasswordConfirm: ''
    })

    const handlerImputChange = (e) => {
        const { name, value } = e.target;
        setUser(before => ({
            ...before,
            [name]: value
        }))
    }

    const handlerSubmit = (e) => {
        post('Account', user).then(response => {
            console.log("data: ", response)
        })
    }

    return (
        <Container component='main' maxWidth='md' justify='center'>
            <div style={style.papper}>
                <Typography component='h1' variant='h5'>
                    Add new user
                </Typography>
                <form style={style.form}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} md={12}>
                            <TextField name='Email' value={user.Email} onChange={handlerImputChange} type='email' variant='outlined' fullWidth label='Insert your email' />
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name='FirstName' value={user.FirstName} onChange={handlerImputChange} variant='outlined' fullWidth label='Insert your first name' />
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name='LastName' value={user.LastName} onChange={handlerImputChange} variant='outlined' fullWidth label='Insert your last name' />
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name='Document' value={user.Document} onChange={handlerImputChange} variant='outlined' fullWidth label='Insert your document' />
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name='Address' value={user.Address} onChange={handlerImputChange} variant='outlined' fullWidth label='Insert your address' />
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name='Password' value={user.Password} onChange={handlerImputChange} type='password' variant='outlined' fullWidth label='Insert your password' />
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name='PasswordConfirm' value={user.PasswordConfirm} onChange={handlerImputChange} type='password' variant='outlined' fullWidth label='Comfirm your password' />
                        </Grid>
                    </Grid>
                    <Grid container justify='center'>
                        <Grid item xs={12} md={12}>
                            <Button type='submit' onClick={handlerSubmit} fullWidth variant='contained' color='primary' size='large' style={style.submit}>
                                Register
                            </Button>
                        </Grid>
                    </Grid>
                </form>
            </div>
        </Container>
    );
};

export default AddUser;