import FilterListIcon from '@mui/icons-material/FilterList';
import {
	Backdrop, Badge, Button, ButtonProps, CircularProgress, FormHelperText, IconButton, Paper, PaperProps,
	Popper, PopperProps, SxProps, Table, TableBody, TableCell, TableCellProps, TableContainer, TableFooter, TableHead,
	TablePagination, TablePaginationProps, TableProps, TableRow, TableRowProps, TableSortLabel, TextField, Theme, Tooltip
} from '@mui/material';
import { styled } from '@mui/material/styles';
import * as React from 'react';

/*
 TODO: sample use
 
import { ExTable, ExTableFilter, IExTableSearchParams } from '../../common/ExTable';

    const [errorMessageByRows, setErrorMessageByRows] = useState('');
    const [rows, setRows] = React.useState<IEvent[]>([]);
    const [isLoading, getData] = useProcessingOnlyOne(async (e: ProcessingAgrs) => {
        const params = e.tag as IExTableSearchParams;
        if (params) {
            try {
                const controller = new MonitoringController();
                var rows = await controller.GetEvents( params);
                setRows(rows);
            }
            catch (err) {
                setErrorMessageByRows('Ошибка получения данных: ' + err);
            }
        }
    }, []);

			<ExTable rows={rows} onChangeSearchParams={getData}
                isLoading={isLoading} errorMessage={errorMessageByRows} columns={[
                    { propertyName: 'name', description: 'Имя', filter: new ExTableFilterDef() },
                    { propertyName: 'surname', description: 'Фамилия', filter: new ExTableFilterDef() },
                    { propertyName: 'patronimic', description: 'Отчество', disibledSort: true },
                ]} propertyToKey='id' orderBy='name' order='asc' />

*/

export type TOrder = 'asc' | 'desc';
export type TAlign = 'inherit' | 'left' | 'center' | 'right' | 'justify';

export class ExTableFilterDef {
	value: string = '';
}

export enum FilterTypeEnum {
	String = "text",
	Date = "date"
}

export class ExTableColumnDef<T> {
	propertyName: string = '';
	description?: string;
	alignHead?: TAlign;
	alignCell?: TAlign;
	filter?: ExTableFilterDef;
	disibledSort?: boolean;
	renderDataCell?: (rowData: T) => JSX.Element;
	filterType?: FilterTypeEnum = FilterTypeEnum.String
}

export class ExTableFilter {

	constructor(public propertyName: string, public value: string) {

	}
}

export class ExTableSearchParams {

	constructor(public orderBy: string,
		public order: TOrder,
		private _page: number,
		public rowsPerPage: number,
		public filters: ExTableFilter[]) {
	}

	get page(): number {
		//потому что пейджинг оперирует страницами, начиная с 0
		return this._page + 1;
	}
	set page(page: number) {
		this._page = page;
	}

}

export interface IExTableProps<T> {
	columns: ExTableColumnDef<T>[];
	propertyToKey: (item: T) => string;
	rowsPerPage?: number;
	page?: number;
	order?: TOrder;
	orderBy?: string;
	rows: T[];
	onChangeSearchParams: (sParams: ExTableSearchParams) => void;
	isLoading: boolean;
	errorMessage?: string;
	total: number;
	rowDef?: ExTableRowDef<T>;
}

export interface ExTableRowDef<T> {
	getClassName?: (item: T) => string;
	getSx?: (item: T) => SxProps<Theme>;
}

interface IFilterProps {
	propertyName: string;
	value: string;
	onChangeFilter: (property: string, newValue: string) => void;
	filteType: FilterTypeEnum;
}

interface IExTableHeadProps<T> {
	columns: ExTableColumnDef<T>[];
	onRequestSort: (property: string) => void;
	onRequestFilter: (filter: ExTableFilter) => void;
	order: TOrder;
	orderBy: string;
	disabledSort: boolean;
	hideFilter: boolean;
}

function TableFilter(props: IFilterProps) {
	const { propertyName, onChangeFilter, filteType } = props;

	const [value, setValue] = React.useState(props.value);
	const [open, setOpen] = React.useState(false);
	const [anchorEl, setAnchorEl] = React.useState<HTMLButtonElement | null>(null);

	const handleFilterOpenOrClose = (event: React.MouseEvent<HTMLButtonElement>) => {
		setAnchorEl(event.currentTarget);
		setOpen(!open);
	};
	const handleFilterReset = () => {
		setOpen(false);
		const def = '';
		setValue(def);
		onChangeFilter(propertyName, def);
	};
	const handleFilterApply = () => {
		setOpen(false);
		onChangeFilter(propertyName, value);
	};

	const handlerChangeValue = (event: React.ChangeEvent<HTMLInputElement>) => {
		setValue(event.currentTarget.value);
	};

	const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
		if (event.key === 'Enter') {
			handleFilterApply();
		}
	};

	return (
		<>
			<Tooltip title='Фильтр'>
				<Badge color="primary" badgeContent={value ? 1 : 0} overlap="circular">
					<IconButton aria-label="filter list" onClick={handleFilterOpenOrClose}>
						<FilterListIcon />
					</IconButton>
				</Badge>
			</Tooltip>
			<StyledFilterPopper open={open} anchorEl={anchorEl} placement='bottom'>
				<StyledFilterPaper>
					<TextField
						value={value}
						margin="normal"
						fullWidth
						label='Значение фильтра'
						type={filteType}
						autoFocus
						onChange={handlerChangeValue}
						onKeyPress={handleKeyDown}
					/>
					<StyledFilterButton variant="outlined" onClick={handleFilterReset} disabled={!(value)}>Сбросить</StyledFilterButton>
					<StyledFilterButton variant="contained" color="primary" onClick={handleFilterApply}>Применить</StyledFilterButton>
				</StyledFilterPaper>
			</StyledFilterPopper>
		</>
	);
}

function ExTableHead<T>(props: IExTableHeadProps<T>) {
	const { columns, order, orderBy, onRequestSort, onRequestFilter,
		disabledSort, hideFilter } = props;

	const createSortHandler = (property: string) => (event: React.MouseEvent<unknown>) => {
		onRequestSort(property);
	};

	const createFilterHandler = (property: string, newValue: string) => {
		const col = columns.find(c => c.propertyName === property);
		if (col && col.filter) {
			col.filter.value = newValue;
			onRequestFilter(new ExTableFilter(col.propertyName, newValue ?? ''));
		}
	};

	const isDisibledSort = (column: ExTableColumnDef<T>): boolean => {
		return disabledSort || (column.disibledSort ?? false);
	};

	return (
		<TableHead>
			<TableRow sx={{ height: '46px' }}>
				{columns.map((column) => (
					<StyledTableCellTh
						key={column.propertyName}
						align={column.alignHead ?? 'left'}
						sortDirection={orderBy === column.propertyName ? order : false}
					>
						{(isDisibledSort(column) ? column.description : <TableSortLabel
							active={orderBy === column.propertyName}
							direction={orderBy === column.propertyName ? order : 'asc'}
							onClick={createSortHandler(column.propertyName)}
						>
							{column.description}
							{orderBy === column.propertyName ? (
								<StyledSpanVisuallyHidden />) : null}
						</TableSortLabel>)}
						{!hideFilter && column.filter && <TableFilter value={column.filter.value}
							onChangeFilter={createFilterHandler} propertyName={column.propertyName}
							filteType={column.filterType ?? FilterTypeEnum.String}
							/>}
					</StyledTableCellTh>
				))}
			</TableRow>
		</TableHead>
	);
}

function RenderDataCell<T>(row: T, column: ExTableColumnDef<T>) {
	return (<>{row[column.propertyName]} </>)
}

const StyledPaper = styled(Paper)<PaperProps>(({ theme }) => ({
	width: '100%',
	marginBottom: theme.spacing(2),
	borderRadius: theme.spacing(1),
}));

const StyledTable = styled(Table)<TableProps>(({ theme }) => ({
	minWidth: 500,
	borderRadius: theme.spacing(1),
	backgroundColor: 'seagreen',
	marginLeft: 'auto',
	marginRight: 'auto',
	emptyCells: 'show',
}));

const StyledSpanVisuallyHidden = styled('span')(() => ({
	border: 0,
	clip: 'rect(0 0 0 0)',
	height: 1,
	margin: -1,
	overflow: 'hidden',
	padding: 0,
	position: 'absolute',
	top: 20,
	width: 1,
}));

const StyledFilterPopper = styled(Popper)<PopperProps>(({ theme }) => ({
	padding: theme.spacing(2),
}));

const StyledFilterPaper = styled(Paper)<PaperProps>(({ theme }) => ({
	padding: theme.spacing(1),
}));

const StyledDivRoot = styled('div')(() => ({
	width: '100%',
	position: 'relative',
}));

const StyledTableRow = styled(TableRow)<TableRowProps>(({ theme }) => ({
	backgroundColor: 'rgba(255,255,255,0.85)',
	borderBottomWidth: theme.spacing(0.5),
	'&:hover': {
		backgroundColor: 'lightseagreen !important',
	},
}));

const StyledFilterButton = styled(Button)<ButtonProps>(({ theme }) => ({
	margin: theme.spacing(0, 1, 0, 0),
}));

const StyledTableCellTh = styled(TableCell)<TableCellProps>(({ theme }) => ({
	borderBottomWidth: theme.spacing(0.5),
	borderColor: 'rgba(200,146,110,0.7)',
	paddingBottom: 0,
	paddingTop: theme.spacing(0.5),
	paddingLeft: theme.spacing(1),
	paddingRight: theme.spacing(0.5),
}));

const StyledTablePagination = styled(TablePagination)<TablePaginationProps>(({ theme }) => ({
	backgroundColor: 'rgba(90,184,152,0.8)',
	borderBottomLeftRadius: theme.spacing(1),
	borderBottomRightRadius: theme.spacing(1),
}));

export function ExTable<T>(props: IExTableProps<T>) {
	const { isLoading, onChangeSearchParams, errorMessage, rows, total, rowDef } = props;

	const [params, setParams] = React.useState(new ExTableSearchParams
		(props.orderBy ?? '', props.order ?? 'asc', (props.page ?? 1) - 1, props.rowsPerPage ?? 5,
			props.columns
			.filter(col => col.filter && col.filter.value)
			.map(col => new ExTableFilter(col.propertyName, col.filter?.value ?? ''))));

	const createSearchParams = (): ExTableSearchParams => {
		const p = new ExTableSearchParams(params.orderBy, params.order, params.page - 1,
			params.rowsPerPage, params.filters);
		return p;
	}
	
	React.useEffect(() => {
		dataProcess(() => createSearchParams());
	}, []);
	
	const dataProcess = (func: () => ExTableSearchParams) => {
		if (isLoading) { return; }
		const sParams = func();
		onChangeSearchParams(sParams);
	}

	const handleRequestSort = (propertyName: string) => {
		dataProcess(() => {
			const isAsc = params.orderBy === propertyName && params.order === 'asc';
			const newOrder = isAsc ? 'desc' : 'asc';

			const p = createSearchParams();
			p.order = newOrder;
			p.orderBy = propertyName;
			p.page = 0;

			setParams(p);

			return p;
		});
	};

	const handleRequestFilter = (filter: ExTableFilter) => {
		dataProcess(() => {
			let newFilters = [...params.filters];
			const f = newFilters.find(f => f.propertyName === filter.propertyName);
			if (f) {
				f.value = filter.value;
			}
			else {
				newFilters.push(filter);
			}
			newFilters = newFilters.filter(f => f.value);;

			const p = createSearchParams();
			p.filters = newFilters;
			p.page = 0;

			setParams(p);

			return p;
		});
	};

	const handleChangePage = async (event: React.MouseEvent<HTMLButtonElement> | null, newPage: number) => {
		dataProcess(() => {

			const p = createSearchParams();
			p.page = newPage;

			setParams(p);

			return p;
		});
	};

	const handleChangeRowsPerPage = (event) => {
		dataProcess(() => {
			const rowsPerPage = parseInt(event.target.value, 10);

			const p = createSearchParams();
			p.page = 0;
			p.rowsPerPage = rowsPerPage;

			setParams(p);

			return p;
		});
	};

	const isEmpty = () => total < 1;

	const getRowsPerPageOptions = (): number[] => {
		if (isEmpty()) { return []; }

		if (total > 10) {
			return [5, 10, 25];
		}
		else if (total > 5) {
			return [5, 10];
		}

		return [];
	};

	const getRowsView = () => {

		if (errorMessage) {
			return (<StyledTableRow>
				<TableCell colSpan={props.columns.length}>
					<FormHelperText sx={{ textAlign: 'center' }} error>{errorMessage}</FormHelperText>
				</TableCell>
			</StyledTableRow>);
		}

		if (isEmpty()) {
			return (<StyledTableRow>
				<TableCell colSpan={props.columns.length}>Нет данных</TableCell>
			</StyledTableRow>);
		}

		return (<>{
			rows.map((row) => {
				return (
					<StyledTableRow
						className={`${rowDef?.getClassName ? rowDef.getClassName(row) : ''}`}
						sx={rowDef?.getSx ? rowDef.getSx(row) : {}}
						hover
						tabIndex={-1}
						key={props.propertyToKey(row)}
					>
						{props.columns.map((column, index) => {
							return (
								<TableCell key={`${column.propertyName}_${index}`}
									align={column.alignCell ?? 'left'}
									sx={{
										paddingBottom: 0,
										paddingTop: 0,
										paddingLeft: (theme) => theme.spacing(1),
										paddingRight: (theme) => theme.spacing(0.5),
									}}
								>{column.renderDataCell ?
									column.renderDataCell(row) : RenderDataCell(row, column)}</TableCell>);
						})}
					</StyledTableRow>
				);
			})}
		</>);
	};

	return (
		<StyledDivRoot>
			<Backdrop sx={{
				position: 'absolute',
				zIndex: (theme) => theme.zIndex.drawer - 1,
				color: '#fff',
			}} open={isLoading}>
				<CircularProgress color="inherit" />
			</Backdrop>
			<StyledPaper>
				<TableContainer>
					<StyledTable
						aria-labelledby="tableTitle"
						size={'medium'}
						aria-label="exTable"
					>
						<ExTableHead
							columns={props.columns}
							order={params.order}
							orderBy={params.orderBy}
							onRequestSort={handleRequestSort}
							onRequestFilter={handleRequestFilter}
							disabledSort={total <= 1}
							hideFilter={errorMessage ? true : false}
						/>
						<TableBody>
							{getRowsView()}
						</TableBody>
						<TableFooter>
							<TableRow sx={{
								padding: 0,
							}}>
								<StyledTablePagination
									colSpan={props.columns.length}
									backIconButtonProps={{ 'aria-label': 'Предыдущая страница' }}
									nextIconButtonProps={{ 'aria-label': 'Следующая страница' }}
									labelRowsPerPage='Кол-во строк на странице'
									labelDisplayedRows={({ from, to, count }) => `${from}-${to} из ${count !== -1 ? count : ('больше чем ' + to)}`}
									rowsPerPageOptions={getRowsPerPageOptions()}
									count={total}
									rowsPerPage={params.rowsPerPage}
									page={params.page - 1}
									onPageChange={handleChangePage}
									onRowsPerPageChange={handleChangeRowsPerPage}
								/>
							</TableRow>
						</TableFooter>
					</StyledTable>					
				</TableContainer>
			</StyledPaper>
		</StyledDivRoot>
	);
}