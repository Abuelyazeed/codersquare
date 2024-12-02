import { useParams } from 'react-router-dom';

function ViewPost() {
  const { id } = useParams();

  return <div>Viewing Post {id}</div>;
}
export default ViewPost;
