export const MainBox = {
  mt: 20,
  gap: '2rem',
  display: 'flex',
  flexDirection: 'column',
  alignItems: 'center'
};

export const ButtonStyle = {
  borderRadius: '20px',
  backgroundColor: 'black'
};

export const ProfileImageContainer = {
  display: 'flex',
  flexDirection: 'column',
  alignItems: 'center',
  gap: '0.5rem'
};

export const ProfileImageUpload = {
  width: '150px',
  height: '150px',
  cursor: 'pointer',
  '& img': {
    transition: 'filter 1s ease'
  },
  '&:hover img': {
    filter: 'grayscale(100%)'
  }
};

export const FormBottomSection = {
  display: 'flex',
  flexDirection: 'column',
  gap: 2,
  mt: 2
};

export const SuggestionBox = {
  display: 'flex',
  alignItems: 'center',
  gap: 1
};

export const ImageUploadSuggestionBox = {
  display: 'flex',
  alignItems: 'center'
};

export const DeleteImageButton = {
  color: 'gray',
  textTransform: 'none'
};

export const RegistrationForm = {
  height: '100vh'
};
