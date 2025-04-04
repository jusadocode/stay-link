import * as Yup from 'yup';

export const validationSchema = Yup.object().shape({
  userName: Yup.string().required('Username is required!'),
  password: Yup.string().required('Password is required!')
});
