import { Button } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { useDispatch } from "react-redux";
import { setConfigurationDialog } from "../../features/configurationSlice";

const ConfigurationFilter = () => {
  return (
    <div className="flex justify-between">
      <div>arama</div>
      <AddButton />
    </div>
  );
};

const AddButton = () => {
  var dispatch = useDispatch();

  const addClick = () => {
    dispatch(
      setConfigurationDialog({
        isUpdate: false,
      })
    );
  };

  return (
    <Button onClick={addClick}>
      <AddIcon className="w-4" />
      Ekle
    </Button>
  );
};

export default ConfigurationFilter;
