import { TableCell, TableHead, TableRow } from "@mui/material";

const TableHeader = () => {
  return (
    <TableHead>
      <TableRow>
        <TableCell>#</TableCell>
        <TableCell>ApplicationName</TableCell>
        <TableCell>Name</TableCell>
        <TableCell>Type</TableCell>
        <TableCell>Value</TableCell>
        <TableCell>IsActive</TableCell>
        <TableCell>Process</TableCell>
      </TableRow>
    </TableHead>
  );
};

export default TableHeader;
