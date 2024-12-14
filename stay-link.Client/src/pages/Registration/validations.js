import { object, ref, string } from "yup";

export const RegistrationSchema = object({
  username: string()
    .required("Username is required.")
    .max(50, "Username cannot exceed 50 characters."),

  email: string()
    .required("Email address is required.")
    .email("Invalid email address."),

  password: string()
    .required("Password is required.")
    .min(8, "Password must be at least 8 characters long.")
    .max(50, "Password cannot exceed 50 characters.")
    .matches(/[A-Z]/, "Password must include at least one uppercase letter.")
    .matches(/[a-z]/, "Password must include at least one lowercase letter.")
    .matches(/\d/, "Password must include at least one number.")
    .matches(
      /[!"#$%&'()*+,-./:;<=>?@[\]^_`{|}~]/,
      "Password must include at least one special character."
    ),

  repeatedPassword: string()
    .required("Please confirm your password.")
    .oneOf([ref("password")], "Passwords must match!")
});
