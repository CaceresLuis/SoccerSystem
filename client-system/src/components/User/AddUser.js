import { Button, Container, Grid, TextField, Typography } from '@mui/material';
import React from 'react';
import style from '../../theme/style'

const AddUser = () => {
    return (
        <Container component='main' maxWidth='md' justify='center'>
            <div style={style.papper}>
                <Typography component='h1' variant='h5'>
                    Add new user
                </Typography>
                <form style={style.form}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} md={6}>
                            <TextField name='FirstName' variant='outlined' fullWidth label='Insert your first name'/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name='LastName' variant='outlined' fullWidth label='Insert your last name'/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name='Document' variant='outlined' fullWidth label='Insert your document'/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name='Address' variant='outlined' fullWidth label='Insert your address'/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name='Password' type='password' variant='outlined' fullWidth label='Insert your password'/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name='PasswordConfirm' type='password' variant='outlined' fullWidth label='Comfirm your password'/>
                        </Grid>
                    </Grid>
                    <Grid container justify='center'>
                        <Grid item xs={12} md={12}>
                            <Button type='submit' fullWidth variant='contained' color='primary' size='large' style={style.submit}>
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