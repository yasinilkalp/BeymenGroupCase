import * as React from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import { styled } from "@mui/material/styles";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import SaveOutlinedIcon from "@mui/icons-material/SaveOutlined";

const CustomizedDialog = styled(Dialog)(({ theme }) => ({
  "& .MuiDialogTitle-root": {
    borderBottom: "1px solid #d9d9d9",
  },
  "& .MuiDialogContent-root": {
    paddingTop: "20px !important",
  },
  "& .MuiPaper-root": {
    padding: "0px",
  },
  "& .MuiDialogActions-root": {
    padding: "12px",
    paddingRight: "24px",
    background: "#f6f6f6",
    borderTop: "1px solid #d9d9d9",
  },
}));

const DialogComponent = (props) => {
  const {
    data,
    title,
    handleClose,
    handleSave,
    maxWidth,
    loading,
    closeButtonText,
    saveButtonText,
    hideSaveButton,
    className,
  } = props;

  return (
    <CustomizedDialog
      open={data != null}
      fullWidth={true}
      maxWidth={maxWidth ?? "sm"}
      onClose={handleClose}
      aria-labelledby="dialog-title"
      className={className ?? ""}
    >
      <DialogTitle id="dialog-title">{title}</DialogTitle>
      <DialogContent>{props.children}</DialogContent>
      <DialogActions>
        <div className="mr-2">
          <Button
            variant="outlined"
            color="inherit"
            size="small"
            onClick={handleClose}
          >
            {closeButtonText ?? "Cancel"}
          </Button>
        </div>
        {!hideSaveButton && (
          <Button
            variant="contained"
            color="success"
            size="small"
            loading={loading}
            onClick={handleSave}
            startIcon={<SaveOutlinedIcon />}
            style={{
              margin: "0px",
            }}
          >
            {saveButtonText ?? "Save"}
          </Button>
        )}
      </DialogActions>
    </CustomizedDialog>
  );
};

export default DialogComponent;
