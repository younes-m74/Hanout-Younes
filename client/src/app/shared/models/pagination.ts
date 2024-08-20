export type Pagination<T> = {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: T[];
}
// This is a generic type that will be used for pagination in the app. It will have a pageIndex, pageSize, count, and data properties. 
// The data property will be of type T, which will be specified when using the Pagination type. 
// This will allow us to paginate any type of data in the app.