// require('hbase')({ host: '127.0.0.1', port: 8080 })
// // Create a table
// .table('Account' )
// .create('account_data', function(err, success){
//   // Insert a record
//   this.put('account_data:name', 'my value', function(err, success){
//     // Read a record
//     this.get('account_data', function(err, [[cell]]){
//       // Validate the result
//       console.log(cell.key);
//       console.log(cell.column);
//       console.log(cell.$);
//     })
//   })
// })

const hbase = require('hbase')
// Instantiate a new client
client = hbase({ host: '127.0.0.1', port: 32778 }) //8080
// Create a table
client
.table('my_table' )
.create('my_column_family', function(err, success){
  // Insert a record
  client
  .table('my_table' )
  .row('my_row')
  .put('my_column_family:my_column', 'my value', function(err, success){
    // Read a record
    client
    .table('my_table' )
    .row('my_row')
    .get('my_column_family', function(err, [cell]){
        console.log(cell);
        console.log(cell?.key);
        console.log(cell?.column);
        console.log(cell?.$);
    })
  })
})