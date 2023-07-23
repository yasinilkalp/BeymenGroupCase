import { Alert, Snackbar } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { setConfigurationMessage } from "../../features/configurationSlice";

const SnackBarComponents = (props) => {
  const dispatch = useDispatch();

  const configurationMessage = useSelector(
    (state) => state.configuration.configurationMessage
  );

  const onClose = () => {
    dispatch(setConfigurationMessage(null));
  };

  return (
    configurationMessage && (
      <Snackbar
        anchorOrigin={{ vertical: "top", horizontal: "right" }}
        open={configurationMessage !== null}
        autoHideDuration={5000}
        onClose={onClose}
      >
        <Alert onClose={onClose} severity={configurationMessage.severity}>
          {configurationMessage.text}
        </Alert>
      </Snackbar>
    )
  );
};
export default SnackBarComponents;
