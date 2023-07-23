import { IconButton, Tooltip } from "@mui/material";
import EditOutlinedIcon from "@mui/icons-material/EditOutlined";
import { useDispatch } from "react-redux";
import { setConfigurationDialog } from "../../features/configurationSlice";

const EditButton = (props) => {
  var dispatch = useDispatch();
  const { item } = props;

  const onEditClick = () => {
    dispatch(setConfigurationDialog({ ...item, isUpdate: true }));
  };

  return (
    <Tooltip title="Edit">
      <IconButton size="small" onClick={onEditClick}>
        <EditOutlinedIcon />
      </IconButton>
    </Tooltip>
  );
};

export default EditButton;
