import { IconButton, Tooltip } from "@mui/material";
import DeleteOutlineOutlinedIcon from "@mui/icons-material/DeleteOutlineOutlined";
import { useDispatch } from "react-redux";
import { Delete } from "../../services/ConfigurationService";
import {
  setConfigurationDataRefresh,
  setConfigurationMessage,
} from "../../features/configurationSlice";

const DeleteButton = (props) => {
  const dispatch = useDispatch();
  const { item } = props;

  const onDeleteClick = () => {
    let key = item.applicationName + "." + item.name;
    Delete(key)
      .then((response) => {
        if (response.data) {
          dispatch(setConfigurationDataRefresh(true));
          dispatch(
            setConfigurationMessage({
              text: "The record has been successfully removed.",
              severity: "success",
            })
          );
        } else {
          dispatch(
            setConfigurationMessage({
              text: "The record could not be removed.",
              severity: "error",
            })
          );
        }
      })
      .catch((error) => {
        dispatch(
          setConfigurationMessage({
            text: error.message,
            severity: "error",
          })
        );
      });
  };
  return (
    <Tooltip title="Delete">
      <IconButton size="small" onClick={onDeleteClick}>
        <DeleteOutlineOutlinedIcon />
      </IconButton>
    </Tooltip>
  );
};

export default DeleteButton;
